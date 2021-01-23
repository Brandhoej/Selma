using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Selma.Core.Domain.Events.Abstractions;
using System;

namespace Selma.Core.Domain.Events
{
    /// <summary>
    ///     Represents different extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service 
        ///     <see cref="IDomainEventDispatcher"/> and implementation <see cref="DomainEventDispatcher"/> and
        ///     adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service 
        ///     <see cref="IDeferredDomainEventDispatcher"/> and implementation <see cref="DeferredDomainEventDispatcher"/>.
        ///     It is required for the <paramref name="serviceCollection"/> to build a <see cref="IServiceProvider"/> with a <see cref="IMediator"/>.
        /// </summary>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the services for the <see cref="IDeferredDomainEventDispatcher"/> service.
        /// </param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddDeferredDomainEventDispatcher(this IServiceCollection serviceCollection)
            => serviceCollection.AddDeferredDomainEventDispatcher(
                provider => new DomainEventDispatcher(provider.GetService<IMediator>()));

        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service 
        ///     <see cref="IDomainEventDispatcher"/> and implementation <typeparamref name="TDispatcherImplementation"/> and
        ///     adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service 
        ///     <see cref="IDeferredDomainEventDispatcher"/> and implementation <see cref="DeferredDomainEventDispatcher"/>.
        ///     It is required for the <paramref name="serviceCollection"/> to build a <see cref="IServiceProvider"/> with a <see cref="IDomainEventQueue"/>.
        /// </summary>
        /// <typeparam name="TDispatcherImplementation">
        ///     The <see cref="IDomainEventDispatcher"/> service to add.
        /// </typeparam>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the services for the <see cref="IDeferredDomainEventDispatcher"/> service.
        /// </param>
        /// <param name="dispatcherFactory">
        ///     The factory that creates the <typeparamref name="TDispatcherImplementation"/>.</param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddDeferredDomainEventDispatcher<TDispatcherImplementation>(this IServiceCollection serviceCollection, Func<IServiceProvider, TDispatcherImplementation> dispatcherFactory)
            where TDispatcherImplementation
            : class
            , IDomainEventDispatcher
        {
            serviceCollection.AddScoped<IDomainEventDispatcher, TDispatcherImplementation>(dispatcherFactory);
            serviceCollection.AddScoped<IDeferredDomainEventDispatcher, DeferredDomainEventDispatcher>(
                provider => new DeferredDomainEventDispatcher(provider.GetService<IDomainEventQueue>(), provider.GetService<IDomainEventDispatcher>()));
            return serviceCollection;
        }

        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service
        ///     <see cref="IDomainEventDispatcher"/> and implementation <see cref="DomainEventDispatcher"/>
        ///     constructed by the <paramref name="dispatcherFactory"/> and
        ///     adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service
        ///     <see cref="IImmediateDomainEventDispatcher"/> and implementation <see cref="ImmediateDomainEventDispatcher"/> and
        ///     adds a <see cref="ServiceDescriptor.Scoped{TService}(Func{IServiceProvider, TService})"/> with the service
        ///     <see cref="IDomainEventQueuer"/> which is just an implicit type conversion for the <see cref="IImmediateDomainEventDispatcher"/>
        ///     which implements the <see cref="IDomainEventQueuer"/>.
        ///     It is required for the <paramref name="serviceCollection"/> to build a <see cref="IServiceProvider"/> with a <see cref="IMediator"/>.
        /// </summary>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the services for the <see cref="IImmediateDomainEventDispatcher"/> service.
        /// </param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddImmediateDomainEventDispatcher(this IServiceCollection serviceCollection)
            => serviceCollection.AddImmediateDomainEventDispatcher(
                provider => new DomainEventDispatcher(provider.GetService<IMediator>()));

        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service
        ///     <see cref="IDomainEventDispatcher"/> and implementation <typeparamref name="TDispatcherImplementation"/>
        ///     constructed by the <paramref name="dispatcherFactory"/> and
        ///     adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service
        ///     <see cref="IImmediateDomainEventDispatcher"/> and implementation <see cref="ImmediateDomainEventDispatcher"/> and
        ///     adds a <see cref="ServiceDescriptor.Scoped{TService}(Func{IServiceProvider, TService})"/> with the service
        ///     <see cref="IDomainEventQueuer"/> which is just an implicit type conversion for the <see cref="IImmediateDomainEventDispatcher"/>
        ///     which implements the <see cref="IDomainEventQueuer"/>.
        /// </summary>
        /// <typeparam name="TDispatcherImplementation">
        ///     The implementation of service type <see cref="IDomainEventDispatcher"/> to use.
        /// </typeparam>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the services for the <see cref="IImmediateDomainEventDispatcher"/> service.
        /// </param>
        /// <param name="dispatcherFactory">
        ///     A factory to construct the <typeparamref name="TDispatcherImplementation"/>.
        /// </param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddImmediateDomainEventDispatcher<TDispatcherImplementation>(this IServiceCollection serviceCollection, Func<IServiceProvider, TDispatcherImplementation> dispatcherFactory)
            where TDispatcherImplementation
            : class
            , IDomainEventDispatcher
        {
            serviceCollection.AddScoped<IDomainEventDispatcher, TDispatcherImplementation>(dispatcherFactory);
            serviceCollection.AddScoped<IImmediateDomainEventDispatcher, ImmediateDomainEventDispatcher>(
                provider => new ImmediateDomainEventDispatcher(provider.GetService<IDomainEventDispatcher>()));
            serviceCollection.AddScoped<IDomainEventQueuer>(
                provider => provider.GetService<IImmediateDomainEventDispatcher>());
            return serviceCollection;
        }

        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service
        ///     <see cref="IDomainEventQueue"/> and implementation <see cref="DomainEventQueue"/> and
        ///     a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with the service
        ///     <see cref="IDomainEventQueuer"/> constructed by implicit conversion between the <see cref="IDomainEventQueue"/>
        ///     which implements the <see cref="IDomainEventQueuer"/> interface.
        /// </summary>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the services for the <see cref="IDomainEventQueue"/> service.
        /// </param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddDomainEventQueue(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDomainEventQueue, DomainEventQueue>();
            serviceCollection.AddScoped<IDomainEventQueuer>(
                provider => provider.GetService<IDomainEventQueue>());
            return serviceCollection;
        }
    }
}
