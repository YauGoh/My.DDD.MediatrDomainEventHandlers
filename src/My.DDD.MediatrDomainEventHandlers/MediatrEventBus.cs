using MediatR;

namespace My.DDD.MediatrDomainEventHandlers
{
    internal class MediatrEventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public MediatrEventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            await _mediator.Publish(new MediatrEventNotification<TEvent>(@event));
        }
    }
}
