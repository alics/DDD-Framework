using Framework.Api.Filters;
using Framework.Application;
using Framework.Core;
using Framework.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Framework.Api
{
    [ApiController]
    [ServiceFilter(typeof(RequestResponseLogActionFilterAttribute))]
    public class ApiControllerWithRequestResponseLog : ControllerBase
    {

    }

    [ApiController]
    public class ApiControllerBase : ApiControllerWithRequestResponseLog
    {
        protected IEventBus EventBus;
        protected ICommandBus CommandBus;
        protected IQueryBus QueryBus;
        protected long SecurityPrincipalId;

        public ApiControllerBase(IEventBus eventBus, ICommandBus commandBus, IQueryBus queryBus)
        {
            EventBus = eventBus;
            CommandBus = commandBus;
            QueryBus = queryBus;

            var securityPrincipalIdCliamIdentity = ClaimsPrincipal.Current?.Identities.FirstOrDefault(n => n.Claims.Any(m => m.Type == "SecurityPrincipalId"));
            if(securityPrincipalIdCliamIdentity !=null)
            {
                var claim = securityPrincipalIdCliamIdentity.Claims.FirstOrDefault(m => m.Type == "SecurityPrincipalId");
                SecurityPrincipalId = long.Parse(claim.Value);
            }
        }
    }
}
