using Microsoft.Extensions.DependencyInjection;
using Selma.Core.Domain.Events.Abstractions;
using Selma.Core.MessageQueue.MediatR;
using System.Reflection;

namespace Selma.Core.Domain.Events
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeferredDomainEventMessageQueue(this IServiceCollection serviceCollection)
            => serviceCollection.AddDeferredMessageQueue<IDomainEvent>();

        public static IServiceCollection AddDeferredDomainEventMessageQueue(this IServiceCollection serviceCollection, params Assembly[] assemblies)
            => serviceCollection.AddDeferredMessageQueue<IDomainEvent>(assemblies);

        public static IServiceCollection AddImmediateDomainEventMessageQueue(this IServiceCollection serviceCollection)
            => serviceCollection.AddImmediateMessageQueue<IDomainEvent>();

        public static IServiceCollection AddImmediateDomainEventMessageQueue(this IServiceCollection serviceCollection, params Assembly[] assemblies)
            => serviceCollection.AddImmediateMessageQueue<IDomainEvent>(assemblies);
    }
}
