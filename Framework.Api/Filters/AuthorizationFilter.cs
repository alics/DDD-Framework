﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Framework.Core.Security;

namespace Framework.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _securityOperationCode;

        public AuthorizationAttribute(string securityOperationCode)
        {
            _securityOperationCode = securityOperationCode;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || user.Identity.IsAuthenticated == false)
            {
                throw new UnauthorizedException();
            }

            var authorizationService = (Core.Security.IAuthorizationService)context.HttpContext.RequestServices
                .GetService(typeof(Core.Security.IAuthorizationService));



            bool isAuthorized = authorizationService.IsAuthorized(user.Identity.Name, _securityOperationCode)
                                                  .ConfigureAwait(false).GetAwaiter().GetResult();


            if (!isAuthorized)
            {
                throw new UnauthorizedException();
            }
        }
    }
}
