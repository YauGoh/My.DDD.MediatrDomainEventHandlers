using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace My.DDD.MediatrDomainEventHandlers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseMediatorDomainEventHandling(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IEventBus, MediatrEventBus>();

            RegisterNotificationHandlers(services, assemblies);

            return services;
        }

        private static void RegisterNotificationHandlers(IServiceCollection services, Assembly[] assemblies)
        {
            var eventTypes = assemblies
                .SelectMany(_ => _.GetTypes())
                .Where(type => !type.IsAbstract &&
                               !type.IsInterface &&
                               type.FindInterfaces((t, _) => t == typeof(IEvent), null).Any());

            foreach (var eventType in eventTypes)
            {
                var mediatrNotification = typeof(MediatrEventNotification<>).MakeGenericType(eventType);
                var @interface = typeof(INotificationHandler<>).MakeGenericType(mediatrNotification);

                var handler = typeof(MediatrEventNotificationHandler<>).MakeGenericType(eventType);

                services.AddTransient(@interface, handler);
            }
        }
    }
}
