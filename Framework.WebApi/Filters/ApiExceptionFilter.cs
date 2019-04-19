using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System;

namespace Framework.WebApi.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var status = HttpStatusCode.InternalServerError;

            var errorMessage = "خطا در انجام عملیات";

            if (context.Exception is ApplicationException)
            {
                errorMessage = context.Exception.Message;
            }

            var apiError = new
            {
                ErrorMessage = errorMessage
            };

            var errorResponse = context.Request.CreateResponse<object>(status, apiError);
            context.Response = errorResponse;

            base.OnException(context);
        }
    }
}
