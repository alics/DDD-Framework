using System.Web.Http;
using Framework.Core.Commands;
using Framework.Core.Events;
using Framework.Core.Queries;

namespace Framework.WebApi.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected readonly ICommandBus CommandBus;
        protected readonly IEventBus EventBus;
        protected readonly IQueryBus QueryBus;
        protected ApiControllerBase(ICommandBus commandBus, IEventBus eventBus, IQueryBus queryBus)
        {
            CommandBus = commandBus;
            EventBus = eventBus;
            QueryBus = queryBus;
        }
    }
}
