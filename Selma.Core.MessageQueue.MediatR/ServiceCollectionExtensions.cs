using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Selma.Core.MessageQueue.Abstractions;
using System;
using System.Reflection;

namespace Selma.Core.MessageQueue.MediatR
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeferredMessageQueue<TMessage>(this IServiceCollection serviceCollection)
            where TMessage
            : class
            , IMessage
            => serviceCollection.AddDeferredMessageQueue<TMessage>(AppDomain.CurrentDomain.GetAssemblies());

        public static IServiceCollection AddDeferredMessageQueue<TMessage>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
            where TMessage
            : class
            , IMessage
        {
            serviceCollection.AddMediatR(assemblies);
            serviceCollection.AddScoped<ISender>(
                provider => provider.GetService<IMediator>());
            serviceCollection.AddScoped<IPublisher>(
                provider => provider.GetService<IMediator>());

            serviceCollection.AddScoped<IDispatcher<TMessage>, Dispatcher<TMessage>>();

            serviceCollection.AddScoped<IDeferredMessageQueue<TMessage>, DeferredMessageQueue<TMessage>>();
            serviceCollection.AddScoped<IMessageQueue<TMessage>>(
                provider => provider.GetService<IDeferredMessageQueue<TMessage>>());
            serviceCollection.AddScoped<IMessageQueueProducer<TMessage>>(
                provider => provider.GetService<IDeferredMessageQueue<TMessage>>());

            return serviceCollection;
        }

        public static IServiceCollection AddImmediateMessageQueue<TMessage>(this IServiceCollection serviceCollection)
            where TMessage
            : class
            , IMessage
            => serviceCollection.AddImmediateMessageQueue<TMessage>(AppDomain.CurrentDomain.GetAssemblies());

        public static IServiceCollection AddImmediateMessageQueue<TMessage>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
            where TMessage
            : class
            , IMessage
        {
            serviceCollection.AddMediatR(assemblies);
            serviceCollection.AddScoped<ISender>(
                provider => provider.GetService<IMediator>());
            serviceCollection.AddScoped<IPublisher>(
                provider => provider.GetService<IMediator>());

            serviceCollection.AddScoped<IDispatcher<TMessage>, Dispatcher<TMessage>>();

            serviceCollection.AddScoped<IImediateMessageQueue<TMessage>, ImmediateMessageQueue<TMessage>>();
            serviceCollection.AddScoped<IMessageQueue<TMessage>>(
                provider => provider.GetService<IImediateMessageQueue<TMessage>>());
            serviceCollection.AddScoped<IMessageQueueProducer<TMessage>>(
                provider => provider.GetService<IImediateMessageQueue<TMessage>>());

            return serviceCollection;
        }
    }
}
