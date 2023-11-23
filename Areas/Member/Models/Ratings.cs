using PROG3050_HMJJ.Models.Account;
using System.ComponentModel.DataAnnotations;


namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Ratings
    {
        [Key]
        public int RatingID { get; set; }


        private int? _Value;


        public int? Value { get { return _Value ?? 1; } set { _Value = value; } }


        public string UserName { get; set; }


        public int GameID { get; set; }
    }
}
