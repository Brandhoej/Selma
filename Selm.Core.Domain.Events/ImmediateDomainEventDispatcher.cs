using System;
using System.Threading.Tasks;
using Selma.Core.Domain.Events.Abstractions;

namespace Selma.Core.Domain.Events
{
    /// <summary>
    ///     Implements immediate <see cref="IDomainEvent"/> dispatching upon enqueueing.
    ///     It is possible to use constructor dependency injection to choose the implementation 
    ///     details of the <see cref="IDomainEventDispatcher"/> used by this instance.
    /// </summary>
    public class ImmediateDomainEventDispatcher
        : IImmediateDomainEventDispatcher
        , IDomainEventQueuer
    {
        /// <summary>
        ///     Initializes a new instance of a <see cref="ImmediateDomainEventDispatcher"/> used to handle <see cref="IDomainEvent"/> disptaching immediately upon enqueueing.
        /// </summary>
        /// <param name="domainEventDispatcher">
        ///     The dispatcher to use when dispatching a <see cref="IDomainEvent"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="domainEventDispatcher"/> is null.
        /// </exception>
        public ImmediateDomainEventDispatcher(IDomainEventDispatcher domainEventDispatcher)
            => DomainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));

        /// <summary>
        ///     The <see cref="IDomainEventDispatcher"/> used to dispatch 
        ///     all <see cref="IDomainEvent"/> in the queue
        /// </summary>
        /// <example>
        ///     Example use case is using MediatR to dispatch.
        /// </example>
        /// <example>
        ///     Example use case is using the dispatcher for disptaching onto a network maybe using Apache Kafka.
        /// </example>
        private IDomainEventDispatcher DomainEventDispatcher { get; }

        /// <summary>
        ///     Dispatches a <see cref="IDomainEvent"/>.
        /// </summary>
        /// <param name="domainEvent">
        ///     The <see cref="IDomainEvent"/> to be dispatched
        /// </param>
        /// <returns>
        ///     A task that represents the dispatch operation.
        /// </returns>
        public Task Dispatch(IDomainEvent domainEvent)
            => DomainEventDispatcher.Dispatch(domainEvent);

        /// <summary>
        ///     By enqueueing the <paramref name="domainEvent"/> it is immediately 
        ///     dispatched through the <see cref="IDomainEventDispatcher"/>.
        /// </summary>
        /// <param name="domainEvent">
        ///     The <see cref="IDomainEvent"/> to dispatch.
        /// </param>
        public void Enqueue(IDomainEvent domainEvent)
            => Dispatch(domainEvent).Wait();
    }
}
