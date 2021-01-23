using MediatR;
using Selma.Core.Domain.Events.Abstractions;
using System;
using System.Threading.Tasks;

namespace Selma.Core.Domain.Events
{
    /// <summary>
    ///     Represents a dispatcher for <see cref="IDomainEvent"/>.
    ///     The implementation uses the <see cref="IMediator"/> as a <see cref="IDomainEvent"/> mediator.
    ///     When <see cref="Dispatch(IDomainEvent)"/> is invoked the <see cref="IMediator"/> publishes the <see cref="IDomainEvent"/>
    ///     to all <see cref="INotificationHandler{TNotification}"/> matching the <see cref="IDomainEvent"/>
    /// </summary>
    public class DomainEventDispatcher
        : IDomainEventDispatcher
    {
        /// <summary>
        ///     The <see cref="IMediator"/> used for dispatching/publishing <see cref="IDomainEvent"/>.
        /// </summary>
        private readonly IMediator m_mediator;

        /// <summary>
        ///     Initializes a new instance of a <see cref="DomainEventDispatcher"/> with a <paramref name="mediator"/> 
        ///     used for dispatching/publishing <see cref="IDomainEvent"/> when <see cref="Dispatch(IDomainEvent)"/> is invoked.
        /// </summary>
        /// <param name="mediator">
        ///     The <see cref="IMediator"/> to use when <see cref="Dispatch(IDomainEvent)"/> is invoked.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="mediator"/> is null.
        /// </exception>
        public DomainEventDispatcher(IMediator mediator)
            => m_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        /// <summary>
        ///     Dispatches the <paramref name="domainEvent"/> by the <see cref="IMediator"/> injected through the constructor.
        /// </summary>
        /// <param name="domainEvent">
        ///     The <see cref="IDomainEvent"/> to disptach by the <see cref="IMediator"/>.
        /// </param>
        /// <returns>
        ///     A task representing the publish operation by the <see cref="IMediator"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="domainEvent"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if no receiver for the <paramref name="domainEvent"/> was found by the <see cref="IMediator"/>.
        /// </exception>
        public Task Dispatch(IDomainEvent domainEvent)
            => m_mediator.Publish(domainEvent ?? throw new ArgumentNullException(nameof(domainEvent)));
    }
}
