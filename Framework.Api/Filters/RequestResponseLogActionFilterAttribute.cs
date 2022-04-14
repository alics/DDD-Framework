using Framework.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Framework.Api.Filters
{
    public class RequestResponseLogActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly IActivityLogger _activityLogger;
        private readonly IConfiguration _configuration;
        private readonly IExceptionLogger _exceptionLogger;
        public RequestResponseLogActionFilterAttribute(IActivityLogger activityLogger,
            IConfiguration configuration,
            IExceptionLogger exceptionLogger)
        {
            _activityLogger = activityLogger;
            _configuration = configuration;
            _exceptionLogger = exceptionLogger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var isActiveApiLogging = _configuration.GetValue<bool>("Logger:ActivityLogger:IsActiveApiLog");
                if (!isActiveApiLogging) return;


                var request = FormatRequest(context);
                _activityLogger.StartActivityLog(context.HttpContext.Request.GetDisplayUrl(), request);
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                if (context.Exception != null)
                {
                    var response = "Error : " + context.Exception.ToString();
                    _activityLogger.EndActivityLog(context.HttpContext.Request.GetDisplayUrl(), response);
                }
                else
                {
                    var isActiveApiLogging = _configuration.GetValue<bool>("Logger:ActivityLogger:IsActiveApiLog");
                    if (!isActiveApiLogging) return;

                    var responseLogData = new { StatudCode = context.HttpContext.Response.StatusCode };
                    var response = JsonConvert.SerializeObject(responseLogData);
                    _activityLogger.EndActivityLog(context.HttpContext.Request.GetDisplayUrl(), response);
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
            }
        }

        public string GetActionArguments(ActionExecutingContext context)
        {
            if (context.ActionArguments == null)
                return "";

            var builder = new StringBuilder();
            var args = context.ActionArguments;

            foreach (var item in args)
            {
                var strItem = JsonConvert.SerializeObject(item);
                builder.AppendLine(strItem);
            }

            return builder.ToString();
        }
        private object FormatRequest(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            var arguments = GetActionArguments(context);

            return new
            {
                request.Method,
                request.QueryString.Value,
                DisplayUrl = request.GetDisplayUrl(),
                request.Scheme,
                request.Host,
                request.Path,
                arguments
            };
        }
    }
}
