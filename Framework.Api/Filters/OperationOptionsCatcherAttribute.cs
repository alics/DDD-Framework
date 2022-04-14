using Framework.Core;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Framework.Api.Filters
{
    public class OperationOptionsCatcherAttribute : ActionFilterAttribute
    {
        private readonly IOperationOptionsService _operationOptionsService;

        public OperationOptionsCatcherAttribute(IOperationOptionsService operationOptionsService)
        {
            _operationOptionsService = operationOptionsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var operationOptions = new OperationOptions()
            {
                CurrentUserId = "customer1"
            };

            _operationOptionsService.Set(operationOptions);
        }
    }
}
