using System;
using Framework.Core.ExceptionHandling;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Framework.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                foreach (var fieldNameKey in context.ModelState.Keys)
                {
                    foreach (var error in context.ModelState[fieldNameKey].Errors)
                    {
                        if (error.ErrorMessage.Contains("required", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new InvalidRequiredArgumentException(fieldNameKey, new Exception(""));
                        }

                        if (error.ErrorMessage.Contains("maximum length", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new InvalidMaxLengthArgumentException(fieldNameKey, new Exception(""));
                        }
                    }
                }
            }
        }
    }
}
