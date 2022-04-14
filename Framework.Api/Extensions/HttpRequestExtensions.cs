using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;

namespace Framework.Api.Extensions
{
    public static class HttpRequestExtensions
	{
		public static Uri GetRawUrl(this HttpRequest request)
		{
			var httpContext = request.HttpContext;

			var requestFeature = httpContext.Features.Get<IHttpRequestFeature>();

			return new Uri(requestFeature.RawTarget);
		}
	}
}
