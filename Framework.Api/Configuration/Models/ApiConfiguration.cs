using System.Collections.Generic;

namespace Framework.Api.Configuration.Models
{
    public class ApiConfiguration
    {
        public string[] ApiVersions { get; set; }
        public string ApiDefaultVersion { get; set; }
        public string[] ApiSupportedLanguages { get; set; }
        public string ApiDefaultLanguage { get; set; }
        public string ApiName { get; set; }
        public string ApiBaseUrl { get; set; }
        public string IdentityServerBaseUrl { get; set; }
        public string FileServerBaseUrl { get; set; }
        public string CamundaServerBaseUrl { get; set; }
        public string OidcApiName { get; set; }
        public string OidcClientId { get; set; }
        public bool IsSwaggerEnabled { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public string ApiRole { get; set; }
        public bool CorsAllowAnyOrigin { get; set; }
        public string[] CorsAllowOrigins { get; set; } = new List<string>().ToArray();
        public bool IncludeErrorDetail { get; set; }
        public int TimeoutInMilliseconds { get; set; }
    }
}