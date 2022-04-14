using System.Threading.Tasks;

namespace Framework.Core
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent eventToHandle);
    }
}
