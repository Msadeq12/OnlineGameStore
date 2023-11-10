namespace PROG3050_HMJJ.Areas.Member.Helper
{
    public static class HelperClass
    {
        public static string QueryUrlParser(List<string> collection, string url, string endpoint)
        {
            
            for (int i = 0; i <= collection.Count - 1; i++)
            {
                url += $"{endpoint}={collection[i]}";

                if (collection.Count > 1)
                {
                    url += "&";
                }
            }

            if(collection.Count > 1)
            {
                url = url.Remove(url.Length - 1);
            }

           
            return url;
        }
    }
}
