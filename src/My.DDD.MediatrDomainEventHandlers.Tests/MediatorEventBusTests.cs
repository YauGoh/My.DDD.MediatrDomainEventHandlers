using Microsoft.Extensions.DependencyInjection;
using Moq;
using My.DDD.MediatrDomainEventHandlers.Tests.Examples;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace My.DDD.MediatrDomainEventHandlers.Tests
{
    public class MediatorEventBusTests
    {
        private readonly IEventBus _bus;

        public MediatorEventBusTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddDomainEvents(Assembly.GetExecutingAssembly())
                .UseMediatorDomainEventHandling(Assembly.GetExecutingAssembly());

            var provider = serviceCollection.BuildServiceProvider();

            _bus = provider.GetRequiredService<IEventBus>();
        }

        [Fact]
        public async Task When_Published_the_event_is_handled()
        {
            var mockSpayableEvent = new Mock<SpyableEvent>();

            await _bus.PublishAsync(mockSpayableEvent.Object);

            mockSpayableEvent.Verify(_ => _.ToBeCalledByHandler(), Times.Once);
        }
    }
}
