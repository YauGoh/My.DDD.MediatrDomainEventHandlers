using MediatR;

namespace My.DDD.MediatrDomainEventHandlers
{
    public class MediatrEventNotification<TDomainEvent> : INotification where TDomainEvent : IEvent
    {
        public MediatrEventNotification(TDomainEvent @event)
        {
            Event = @event;
        }

        public TDomainEvent Event { get; }
    }
}
