using System;
using System.Collections.Specialized;
using System.Linq;

namespace Framework.Web.Helpers
{
    public static class RequestExtentions
    {
        public static string GetUserAgent(NameValueCollection requestData)
        {
            return requestData["HTTP_USER_AGENT"];
        }

        public static string GetUserIP(NameValueCollection requestData)
        {
            var ip = !String.IsNullOrEmpty(requestData["HTTP_X_FORWARDED_FOR"])
                ? requestData["HTTP_X_FORWARDED_FOR"]
                : requestData["REMOTE_ADDR"];

            if (ip.Contains(","))
            {
                ip = ip.Split(',').First().Trim();
            }

            return ip;
        }
    }
}
