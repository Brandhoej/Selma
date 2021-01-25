using Microsoft.Extensions.DependencyInjection;
using Selma.Core.MessageQueue.Abstractions;
using Selma.Core.Infrastructure.Events.Abstractions;
using Selma.Core.MessageQueue.Kafka;

namespace Selma.Core.Infrastructure.Events
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaIntegrationEventDispatcher(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDispatcher<IIntegrationEvent>, Dispatcher<IIntegrationEvent>>(
                provider => new Dispatcher<IIntegrationEvent>());
            serviceCollection.AddScoped<IDeferredMessageQueue<IIntegrationEvent>, DeferredMessageQueue<IIntegrationEvent>>(
                provider => new DeferredMessageQueue<IIntegrationEvent>(provider.GetService<IDispatcher<IIntegrationEvent>>()));
            serviceCollection.AddScoped<IMessageQueue<IIntegrationEvent>>(
                provider => provider.GetService<IDeferredMessageQueue<IIntegrationEvent>>());
            serviceCollection.AddScoped<IMessageQueueProducer<IIntegrationEvent>>(
                provider => provider.GetService<IDeferredMessageQueue<IIntegrationEvent>>());
            return serviceCollection;
        }
    }
}
