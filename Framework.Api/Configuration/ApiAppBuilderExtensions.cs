using System.Collections.Generic;
using Framework.Api.Configuration.Models;
using Microsoft.AspNetCore.Builder;

namespace Framework.Api.Configuration
{
    public static class ApiAppBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUiInApi(this IApplicationBuilder app, ApiConfiguration apiConfiguration, bool useOauth)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var apiVersion in apiConfiguration.ApiVersions)
                {
                    c.SwaggerEndpoint($"{apiConfiguration.ApiBaseUrl}/swagger/{apiVersion}/swagger.json", $"{apiConfiguration.ApiName} {apiVersion}");
                    c.RoutePrefix = string.Empty;
                }

                if (useOauth)
                {
                    c.OAuthClientId(apiConfiguration.OidcClientId);
                    c.OAuthAppName(apiConfiguration.ApiName);
                    c.OAuthUsePkce();
                }
            });
            return app;
        }

        public static IApplicationBuilder UseCustomHeaderInApi(this IApplicationBuilder app, KeyValuePair<string, string> header)
        {
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add(header.Key, header.Value);
                await next();
            });
            return app;
        }
    }
}