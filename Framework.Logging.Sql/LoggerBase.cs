using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Framework.Logging.Sql
{
    public abstract class LoggerBase
    {
        protected string GetUserIdentity()
        {
            if(ClaimsPrincipal.Current == null)
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

        protected string GetClientIp()
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
    }
}