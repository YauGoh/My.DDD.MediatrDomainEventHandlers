using System.Threading.Tasks;

namespace My.DDD.MediatrDomainEventHandlers.Tests.Examples
{
    public class SpyableEventHandler : IEventHandler<SpyableEvent>
    {
        public Task HandleAsync(SpyableEvent @event) => Task.Run(@event.ToBeCalledByHandler);
    }
}
