using Framework.Api.Configuration.Models;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Framework.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Api.Configuration
{
    public static class ApiServiceCollectionExtensions
    {
        public static T AddAppSettingsConfiguration<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            var apiConfiguration = configuration.GetSection(typeof(T).Name).Get<T>();
            services.AddSingleton(apiConfiguration);
            return apiConfiguration;
        }

        public static IServiceCollection AddAuthenticationToApi(this IServiceCollection services, ApiConfiguration apiConfiguration)
        {
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.ApiName = apiConfiguration.OidcApiName;
                    options.RequireHttpsMetadata = apiConfiguration.RequireHttpsMetadata;
                    options.Authority = apiConfiguration.IdentityServerBaseUrl;
                });
            return services;
        }

        public static IServiceCollection AddSwaggerGenToApi(this IServiceCollection services, ApiConfiguration apiConfiguration, bool useOauth)
        {
            services.AddSwaggerGen(options =>
            {
                foreach (var apiVersion in apiConfiguration.ApiVersions)
                {
                    options.SwaggerDoc(apiVersion, new OpenApiInfo {Title = apiConfiguration.ApiName, Version = apiVersion});
                }

                if (useOauth)
                {
                    options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{apiConfiguration.IdentityServerBaseUrl}/connect/authorize"),
                                TokenUrl = new Uri($"{apiConfiguration.IdentityServerBaseUrl}/connect/token"),
                                Scopes = new Dictionary<string, string>
                                {
                                    {apiConfiguration.OidcApiName, apiConfiguration.ApiName}
                                }
                            }
                        }
                    });
                    options.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();
                }
                
                options.OperationFilter<SwaggerRequestHeaderFilter>();
                // options.OperationFilter<RemoveVersionFromParameterFilter>();
                // options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                options.EnableAnnotations();
            });
            return services;
        }

        public static IServiceCollection AddControllersWithNewtonsoftJsonAndNamingStrategyToApi(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new LongConverter());
                options.SerializerSettings.Converters.Add(new NullabeLongConverter());
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        ProcessDictionaryKeys = false,
                        OverrideSpecifiedNames = false,
                        ProcessExtensionDataNames = false
                    }
                };
            });
            return services;
        }

        public static IServiceCollection AddApiVersioningToApi(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            return services;
        }

        public static IServiceCollection AddRequestLocalizationToApi(this IServiceCollection services, ApiConfiguration apiConfiguration)
        {
            //services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = apiConfiguration.ApiSupportedLanguages.Select(language => new CultureInfo(language)).ToList();
                options.DefaultRequestCulture = new RequestCulture(apiConfiguration.ApiDefaultLanguage, apiConfiguration.ApiDefaultLanguage);
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            return services;
        }
    }
}