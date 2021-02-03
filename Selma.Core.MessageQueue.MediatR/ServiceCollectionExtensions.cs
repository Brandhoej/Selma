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
            return serviceCollection.AddMediatR<TMessage>(assemblies)
                .AddScoped<IDeferredMessageQueue<TMessage>, DeferredMessageQueue<TMessage>>()
                .AddScoped<IMessageQueue<TMessage>>(provider => provider.GetService<IDeferredMessageQueue<TMessage>>())
                .AddScoped<IMessageQueueProducer<TMessage>>(provider => provider.GetService<IDeferredMessageQueue<TMessage>>());
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
            return serviceCollection.AddMediatR<TMessage>(assemblies)
                .AddScoped<IImediateMessageQueue<TMessage>, ImmediateMessageQueue<TMessage>>()
                .AddScoped<IMessageQueue<TMessage>>(provider => provider.GetService<IImediateMessageQueue<TMessage>>())
                .AddScoped<IMessageQueueProducer<TMessage>>(provider => provider.GetService<IImediateMessageQueue<TMessage>>());
        }

        private static IServiceCollection AddMediatR<TMessage>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
            where TMessage
            : class
            , IMessage
        {
            serviceCollection.AddMediatR(assemblies)
                .AddScoped<ISender>(provider => provider.GetService<IMediator>())
                .AddScoped<IPublisher>(provider => provider.GetService<IMediator>())
                .AddScoped<IDispatcher<TMessage>, Dispatcher<TMessage>>();
            return serviceCollection;
        }
    }
}
