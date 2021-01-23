using Selma.Core.Domain.Events.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Selma.Core.Domain.Events
{
    /// <summary>
    ///     Implements deferrend <see cref="IDomainEvent"/> dispatching.
    ///     It is possible to use constructor dependency injection to choose the implementation details of the 
    ///     <see cref="IDomainEventQueue"/> and the <see cref="IDomainEventDispatcher"/> used by this instance.
    /// </summary>
    /// <example>
    ///     Example use case is before the SaveChanges of the DbContext is called.
    /// </example>
    public class DeferredDomainEventDispatcher
        : IDeferredDomainEventDispatcher
    {
        /// <summary>
        ///     The queue of domain events which must be dispatched at the next DispatchAll call
        /// </summary>
        private readonly IDomainEventQueue m_domainEventQueue;

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
        private readonly IDomainEventDispatcher m_domainEventDispatcher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeferredDomainEventDispatcher"/> class that is empty 
        ///     using the <paramref name="domainEventDispatcher"/> as the dispatcher for every domain event.
        ///     The <see cref="IDomainEventQueue"/> will be the <see cref="DomainEventQueue"/>.
        /// </summary>
        /// <param name="domainEventDispatcher">
        ///     The <see cref="IDomainEventDispatcher"/> used to dispatch all events from the <see cref="IDomainEventQueue"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="domainEventDispatcher"/> is null.
        /// </exception>
        public DeferredDomainEventDispatcher(IDomainEventDispatcher domainEventDispatcher)
            : this(new DomainEventQueue(), domainEventDispatcher)
        { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeferredDomainEventDispatcher"/> class that is empty 
        ///     using the <paramref name="domainEventQueue"/> as the domain event queue storing the <see cref="IDomainEvent"/>
        ///     and <paramref name="domainEventDispatcher"/> as the dispatcher for every domain event.
        /// </summary>
        /// <param name="domainEventQueue">
        ///     The <see cref="IDomainEventQueue"/> used to store all <see cref="IDomainEvent"/> which requires to be dispatched.
        /// </param>
        /// <param name="domainEventDispatcher">
        ///     The <see cref="IDomainEventDispatcher"/> used to dispatch all events from the <see cref="IDomainEventQueue"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     One of the actual parameters are null.
        /// </exception>
        public DeferredDomainEventDispatcher(IDomainEventQueue domainEventQueue, IDomainEventDispatcher domainEventDispatcher)
        {
            m_domainEventQueue = domainEventQueue ?? throw new ArgumentNullException(nameof(domainEventQueue));
            m_domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        }

        /// <summary>
        ///     Dispatches a <see cref="IDomainEvent"/>.
        /// </summary>
        /// <param name="domainEvent">
        ///     The <see cref="IDomainEvent"/> to be dispatched.
        /// </param>
        /// <returns>
        ///     A task that represents the dispatch operation.
        /// </returns>
        public Task Dispatch(IDomainEvent domainEvent)
            => m_domainEventDispatcher.Dispatch(domainEvent);

        /// <summary>
        ///     Dispatches all domain events in the <see cref="Queue{IDomainEvent}"/> 
        ///     using the <see cref="IDomainEventDispatcher"/>
        /// </summary>
        /// <returns>
        ///     A task representing the async dispatching of all <see cref="IDomainEvent"/> in the queue
        /// </returns>
        public async Task DispatchAll()
        {
            while (m_domainEventQueue.Count > 0)
            {
                await Dispatch(m_domainEventQueue.Dequeue());
            }
        }

        /// <summary>
        ///     Enques the <paramref name="domainEvent"/> into the queue such that it is
        ///     ready to get dispatched by the <see cref="IDomainEventDispatcher"/> when
        ///     <see cref="DispatchAll"/> is called
        /// </summary>
        /// <param name="domainEvent">
        ///     The <see cref="IDomainEvent"/> which will be enqueued into the <see cref="IDomainEventQueue"/>
        /// </param>
        public void Enqueue(IDomainEvent domainEvent)
            => m_domainEventQueue.Enqueue(domainEvent);
    }
}
