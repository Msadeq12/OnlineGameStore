using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Net;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PROG3050_HMJJ.Areas.Identity.Pages;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using Azure.Core;

namespace PROG3050_HMJJ.Recaptcha
{
    public class ReCaptchaValidator : Attribute
    {
        private  string _ReCaptchaSecret;
        private readonly string _ReCaptchaSiteKey;
        public List<string> ErrorCodes { get; set; }



        public bool ReCaptchaValide(HttpRequest request, string reCaptchaSecret = "6LcIS5koAAAAAMKWRYt3A-bjYPjmy3mbLLlA621e")
        {
            _ReCaptchaSecret = reCaptchaSecret;
            var sb = new StringBuilder();
            sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");
            sb.Append(_ReCaptchaSecret);
            sb.Append("&response=");
            sb.Append(request.Form["g-recaptcha-response"]);

            //make the api call and determine validity
            using (var client = new WebClient())
            {
                var uri = sb.ToString();
                var json = client.DownloadString(uri);
                var serializer = new DataContractJsonSerializer(typeof(RecaptchaApiResponse));
                var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                var result = serializer.ReadObject(ms) as RecaptchaApiResponse;

                if (result == null)
                {
                    return false;
                }
                else if (result.ErrorCodes != null)
                {
                    foreach (var code in result.ErrorCodes)
                    {
                        this.ErrorCodes.Add(code.ToString());
                    }
                    return false;
                }
                else if (!result.Success)
                {
                    return false;
                }
                else //-- If successfully verified.
                {
                    return true;
                }
            }
        }
       
    }
}
[DataContract]
public class RecaptchaApiResponse
{
    [DataMember(Name = "success")]
    public bool Success;

    [DataMember(Name = "error-codes")]
    public List<string> ErrorCodes;
}

        //private readonly string _secretKey;

        //public RecaptchaValidationAttribute(string secretKey= "6LcIS5koAAAAAMKWRYt3A-bjYPjmy3mbLLlA621e")
        //{
        //    _secretKey = secretKey;
        //}

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    var httpContextAccessor = validationContext.GetRequiredService<IHttpContextAccessor>();
        //    var request = httpContextAccessor.HttpContext.Request;
        //    var recaptchaResponse = value.ToString();

        //    if (string.IsNullOrWhiteSpace(recaptchaResponse))
        //    {
        //        return new ValidationResult(ErrorMessage);
        //    }

        //    var client = new WebClient();
        //    var result = client.UploadValues("https://www.google.com/recaptcha/api/siteverify", new NameValueCollection
        //    {
        //        { "secret", _secretKey },
        //        { "response", recaptchaResponse },
        //    });

        //    var response = client.UploadValues("https://www.google.com/recaptcha/api/siteverify", new NameValueCollection
        //    {
        //        { "secret", _secretKey },
        //        { "response", recaptchaResponse },
        //    });

        //    var responseString = System.Text.Encoding.Default.GetString(response);
        //    var recaptchaResult = Newtonsoft.Json.JsonConvert.DeserializeObject<RecaptchaResponse>(responseString);

        //    if (recaptchaResult == null || !recaptchaResult.Success)
        //    {
        //        return new ValidationResult(ErrorMessage);
        //    }

        //    return ValidationResult.Success;
        //}
  

    public class RecaptchaResponse
    {
        public bool Success { get; set; }
        public double Score { get; set; }
        public string Action { get; set; }
        public DateTime Challenge_ts { get; set; }
        public string Hostname { get; set; }
        public List<string> ErrorCodes { get; set; }
    }



