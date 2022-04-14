using System;
using System.Net;
using System.Threading.Tasks;
using Framework.Api.Configuration.Models;
using Framework.Core;
using Framework.Core.ApiStandardResults;
using Framework.Core.ExceptionHandling;
using Framework.Core.Logging;
using Microsoft.AspNetCore.Http;

namespace Framework.Api.Middlewares
{
    public class ExceptionAdapterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiConfiguration _apiConfiguration;

        public ExceptionAdapterMiddleware(RequestDelegate next, ApiConfiguration apiConfiguration)
        {
            _next = next;
            _apiConfiguration = apiConfiguration;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            await RollBackTransaction(context);

            LogException(context, exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (exception is IBusinessException businessException)
            {
                var error = new ApiError
                {
                    Code = businessException.GetCode(),
                    Message = businessException.GetMessage(),
                    Detail = _apiConfiguration.IncludeErrorDetail ? businessException.ToString() : null
                };

                await context.Response.WriteAsync(ApiResult.StandardError(error).ToString());
                return;
            }

            var unknownError = new ApiError
            {
                Code = (int)CrmGeneralErrorType.UnknownError,
                Message = "UnknownError!",
                Detail = _apiConfiguration.IncludeErrorDetail ? exception.ToString() : null
            };

            await context.Response.WriteAsync(ApiResult.StandardError(unknownError).ToString());
        }

        private void LogException(HttpContext context, Exception exception)
        {
            try
            {
                var exceptionLogger = (IExceptionLogger)context.RequestServices.GetService(typeof(IExceptionLogger));

                if (exceptionLogger != null)
                {
                    exceptionLogger.Log(exception);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task RollBackTransaction(HttpContext context)
        {
            try
            {
                var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
                if (unitOfWork != null)
                    await unitOfWork.RollbackAsync();
            }
            catch (Exception ex)
            {
                try
                {
                    var exceptionLogger = (IExceptionLogger)context.RequestServices.GetService(typeof(IExceptionLogger));
                    if (exceptionLogger != null)
                    {
                        exceptionLogger.Log(ex);
                    }
                }
                catch(Exception ex2)
                {
                    
                }
            }
        }
    }
}
