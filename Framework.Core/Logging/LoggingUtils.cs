using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Framework.Core.Logging
{
    public static class LoggingUtils
    {
        public static string GetUserIdentity()
        {
            if (ClaimsPrincipal.Current == null)
            {
                return "";
            }

            var userIdentity = ClaimsPrincipal.Current.Identities.FirstOrDefault(n => n.Claims.Any(m => m.Type == "UserIdentity"));

            if (userIdentity != null)
            {
                var claim = userIdentity.Claims.FirstOrDefault(m => m.Type == "UserIdentity");

                if (claim != null)
                {
                    return claim.Value;
                }
            }

            return "";
        }

        public static string GetClientIp()
        {
            if (ClaimsPrincipal.Current == null)
            {
                return "";
            }

            var ip = ClaimsPrincipal.Current.Identities.FirstOrDefault(n => n.Claims.Any(m => m.Type == "IP"));

            if (ip != null)
            {
                var claim = ip.Claims.FirstOrDefault(m => m.Type == "IP");

                if (claim != null)
                {
                    return claim.Value;
                }
            }

            return "";
        }

        public static string GetApplicationName(IConfiguration configuration)
        {
            return configuration.GetSection("Logger:ApplicationName").Value;
        }
    }
}
