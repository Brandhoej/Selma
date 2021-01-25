using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Selma.Core.Domain.Events.Abstractions;
using Selma.Core.MessageQueue.Abstractions;
using Selma.Core.MessageQueue.MediatR;

namespace Selma.Core.Domain.Events
{
    /// <summary>
    ///     Represents different extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeferredDomainEventDispatcher(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDispatcher<IDomainEvent>, Dispatcher<IDomainEvent>>(
                provider => new Dispatcher<IDomainEvent>(provider.GetService<IMediator>()));
            serviceCollection.AddScoped<IDeferredMessageQueue<IDomainEvent>, DeferredMessageQueue<IDomainEvent>>(
                provider => new DeferredMessageQueue<IDomainEvent>(provider.GetService<IDispatcher<IDomainEvent>>()));
            serviceCollection.AddScoped<IMessageQueue<IDomainEvent>>(
                provider => provider.GetService<IDeferredMessageQueue<IDomainEvent>>());
            serviceCollection.AddScoped<IMessageQueueProducer<IDomainEvent>>(
                provider => provider.GetService<IDeferredMessageQueue<IDomainEvent>>());
            return serviceCollection;
        }

        public static IServiceCollection AddImmediateDomainEventDispatcher(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDispatcher<IDomainEvent>, Dispatcher<IDomainEvent>>(
                provider => new Dispatcher<IDomainEvent>(provider.GetService<IMediator>()));
            serviceCollection.AddScoped<IImediateMessageQueue<IDomainEvent>, ImmediateMessageQueue<IDomainEvent>>(
                provider => new ImmediateMessageQueue<IDomainEvent>(provider.GetService<IDispatcher<IDomainEvent>>()));
            serviceCollection.AddScoped<IMessageQueue<IDomainEvent>>(
                provider => provider.GetService<IImediateMessageQueue<IDomainEvent>>());
            serviceCollection.AddScoped<IMessageQueueProducer<IDomainEvent>>(
                provider => provider.GetService<IImediateMessageQueue<IDomainEvent>>());
            return serviceCollection;
        }
    }
}
