using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Framework.Api.Configuration.Models;
using Framework.Api.Extensions;
using Microsoft.AspNetCore.Http;

namespace Framework.Api.Middlewares
{
    public class SetClaimsToThreadAdapterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiConfiguration _apiConfiguration;

        public SetClaimsToThreadAdapterMiddleware(RequestDelegate next, ApiConfiguration apiConfiguration)
        {
            _next = next;
            _apiConfiguration = apiConfiguration;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentities(new List<ClaimsIdentity>()
                {
                    new ClaimsIdentity(new List<Claim>{ new Claim("UserIdentity", httpContext.User.Identity.Name??"") }),
                    new ClaimsIdentity(new List<Claim>{ new Claim("IP", httpContext.GetClientIPAddress()) })
                });

            var authorizationService = (Core.Security.IAuthorizationService)httpContext.RequestServices
                .GetService(typeof(Core.Security.IAuthorizationService));

            if (httpContext.User.Identity != null && !string.IsNullOrEmpty(httpContext.User.Identity.Name))
            {
                
                var securityPrincipalId = authorizationService.GetSecurityPrincipleIdOfCurrentUser(httpContext.User.Identity.Name);

                claimsPrincipal.AddIdentities(new List<ClaimsIdentity>()
                {
                    new ClaimsIdentity(new List<Claim>{ new Claim("SecurityPrincipalId", securityPrincipalId.ToString()) })
                });
            }

            Thread.CurrentPrincipal = claimsPrincipal;

            await _next(httpContext);
        }
    }
}
