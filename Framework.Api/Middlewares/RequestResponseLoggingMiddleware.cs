using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Framework.Api.Extensions;
using Framework.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;

namespace Framework.Api.Middlewares
{
    //internal class RequestResponseLoggingMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly ILogger _logger;
    //    private readonly IActivityLogger _activityLogger;

    //    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger, IActivityLogger activityLogger)
    //    {
    //        _next = next;
    //        _logger = logger;
    //        _activityLogger = activityLogger;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        var request = await FormatRequest(context.Request);
    //        _activityLogger.StartActivityLog(context.Request.GetDisplayUrl(), request);

    //        await _next(context);

    //        var response = await FormatResponse(context.Response);
    //        _activityLogger.EndActivityLog(context.Request.GetDisplayUrl(), response);
    //    }

    //    private async Task<object> FormatRequest(HttpRequest request)
    //    {
    //        request.EnableBuffering();

    //        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

    //        await request.Body.ReadAsync(buffer, 0, buffer.Length);

    //        var bodyAsText = Encoding.UTF8.GetString(buffer);

    //        request.Body = new MemoryStream(buffer);

    //        return new
    //        {
    //            request.Method,
    //            request.QueryString.Value,
    //            DisplayUrl = request.GetDisplayUrl(),
    //            request.Scheme,
    //            request.Host,
    //            request.Path,
    //            bodyAsText
    //        };
    //    }

    //    private async Task<object> FormatResponse(HttpResponse response)
    //    {
    //        response.Body.Seek(0, SeekOrigin.Begin);

    //        var text = await new StreamReader(response.Body, Encoding.UTF8).ReadToEndAsync();

    //        response.Body.Seek(0, SeekOrigin.Begin);

    //        return new
    //        {
    //            response.ContentType,
    //            response.StatusCode,
    //            Response = text
    //        };
    //    }
    //}
}