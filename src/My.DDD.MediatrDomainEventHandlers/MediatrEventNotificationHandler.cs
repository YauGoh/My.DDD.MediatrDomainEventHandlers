using MediatR;

namespace My.DDD.MediatrDomainEventHandlers
{
    internal class MediatrEventNotificationHandler<TDomainEvent> : INotificationHandler<MediatrEventNotification<TDomainEvent>> where TDomainEvent : IEvent
    {
        private readonly IEnumerable<IEventHandler<TDomainEvent>> _handlers;

        public MediatrEventNotificationHandler(IEnumerable<IEventHandler<TDomainEvent>> handlers)
        {
            _handlers = handlers;
        }

        public async Task Handle(MediatrEventNotification<TDomainEvent> notification, CancellationToken cancellationToken)
        {
            var tasks = _handlers.Select(hander => hander.HandleAsync(notification.Event));

            await Task.WhenAll(tasks);
        }
    }
}
