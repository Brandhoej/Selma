using Microsoft.Extensions.DependencyInjection;
using Selma.Core.Domain.Events.Abstractions;
using Selma.Core.MessageQueue.MediatR;
using System;
using System.Reflection;

namespace Selma.Core.Domain.Events
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeferredDomainEventMessageQueue<TMessage>(this IServiceCollection serviceCollection)
            => serviceCollection.AddDeferredDomainEventMessageQueue(AppDomain.CurrentDomain.GetAssemblies());

        public static IServiceCollection AddDeferredDomainEventMessageQueue(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            serviceCollection.AddDeferredMessageQueue<IDomainEvent>(assemblies);
            return serviceCollection;
        }

        public static IServiceCollection AddImmediateDomainEventMessageQueue(this IServiceCollection serviceCollection)
            => serviceCollection.AddImmediateDomainEventMessageQueue(AppDomain.CurrentDomain.GetAssemblies());

        public static IServiceCollection AddImmediateDomainEventMessageQueue(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            serviceCollection.AddImmediateMessageQueue<IDomainEvent>(assemblies);
            return serviceCollection;
        }
    }
}
