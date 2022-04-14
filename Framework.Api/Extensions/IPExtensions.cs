using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Framework.Api.Extensions
{
    public static class IPExtensions
	{
		public static string GetClientIPAddress(this HttpContext context)
		{
			string ip = string.Empty;
			if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
			{
				ip = context.Request.Headers["X-Forwarded-For"];
			}
			else
			{
				var feature = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>();
				if (feature != null && feature.RemoteIpAddress != null)
				{
					ip = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
				}
			}

			return ip;
		}
	}
}
