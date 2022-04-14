using System.Collections.Generic;
using System.Linq;
using Framework.Api.Configuration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Api.Filters
{
    public class SwaggerAuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly ApiConfiguration _apiConfiguration;

        public SwaggerAuthorizeCheckOperationFilter(ApiConfiguration apiConfiguration)
        {
            _apiConfiguration = apiConfiguration;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var isAuthorized = context.MethodInfo.DeclaringType != null
                               && (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                                   || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

            if (!isAuthorized) return;

            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "OAuth2"
                            }
                        }
                    ] = new[] {_apiConfiguration.OidcApiName}
                }
            };
        }
    }
}