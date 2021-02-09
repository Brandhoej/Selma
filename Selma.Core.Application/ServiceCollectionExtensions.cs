using Microsoft.Extensions.DependencyInjection;
using Selma.Core.Application.Abstractions;
using System;

namespace Selma.Core.Application
{
    /// <summary>
    ///     Represents different extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with
        ///     the service <see cref="IActor"/> and implementation <typeparamref name="TActorImplementation"/>.
        /// </summary>
        /// <typeparam name="TActorImplementation">
        ///     The <see cref="Actor"/> to add to the <paramref name="serviceCollection"/>.
        /// </typeparam>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the <see cref="IActor"/> service with <typeparamref name="TActorImplementation"/> implementation.
        /// </param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddActor<TActorImplementation>(this IServiceCollection serviceCollection)
            where TActorImplementation
            : Actor
            => serviceCollection.AddScoped<TActorImplementation>();

        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with
        ///     the service <typeparamref name="TActorService"/> and implementation <typeparamref name="TActorImplementation"/>.
        /// </summary>
        /// <typeparam name="TActorService">
        ///     The <see cref="IActor"/> service to add.
        /// </typeparam>
        /// <typeparam name="TActorImplementation">
        ///     The implementation of the <typeparamref name="TActorService"/>.
        /// </typeparam>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the <typeparamref name="TActorService"/> service with <typeparamref name="TActorImplementation"/> implementation.
        /// </param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddActor<TActorService, TActorImplementation>(this IServiceCollection serviceCollection)
            where TActorImplementation
            : class
            , TActorService
            where TActorService
            : class
            , IActor
            => serviceCollection.AddScoped<TActorService, TActorImplementation>();

        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with
        ///     the service <see cref="IActor"/> and implementation <typeparamref name="TActorImplementation"/>.
        /// </summary>
        /// <typeparam name="TActorImplementation">
        ///     The <see cref="Actor"/> to add to the <paramref name="serviceCollection"/>.
        /// </typeparam>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the <see cref="IActor"/> service with <typeparamref name="TActorImplementation"/> implementation.
        /// </param>
        /// <param name="implementationFactory">
        ///     The factory that creates the <typeparamref name="TActorImplementation"/>.
        /// </param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddActor<TActorImplementation>(this IServiceCollection serviceCollection, Func<IServiceProvider, TActorImplementation> implementationFactory)
            where TActorImplementation
            : Actor
            => serviceCollection.AddScoped(implementationFactory);

        /// <summary>
        ///     Adds a <see cref="ServiceDescriptor.Scoped{TService, TImplementation}"/> with
        ///     the service <typeparamref name="TActorService"/> and implementation <typeparamref name="TActorImplementation"/>.
        /// </summary>
        /// <typeparam name="TActorService">
        ///     The <see cref="IActor"/> service to add.</typeparam>
        /// <typeparam name="TActorImplementation">
        ///     The implementation of the <typeparamref name="TActorService"/>.
        /// </typeparam>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection"/> to add the <typeparamref name="TActorService"/> service with <typeparamref name="TActorImplementation"/> implementation.
        /// </param>
        /// <param name="implementationFactory">
        ///     The factory that creates the <typeparamref name="TActorImplementation"/>.
        /// </param>
        /// <returns>
        ///     A reference to the <paramref name="serviceCollection"/> after the operation has completed.
        /// </returns>
        public static IServiceCollection AddActor<TActorService, TActorImplementation>(this IServiceCollection serviceCollection, Func<IServiceProvider, TActorImplementation> implementationFactory)
            where TActorImplementation
            : class
            , TActorService
            where TActorService
            : Actor
            => serviceCollection.AddScoped<TActorService, TActorImplementation>(implementationFactory);
    }
}
