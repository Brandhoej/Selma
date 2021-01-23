using MediatR;
using Selma.Core.Domain.Events.Abstractions;
using System;

namespace Selma.Core.Domain.Events
{
    /// <summary>
    ///     A possible base class for domain specific events.
    ///     The implementation inherits <see cref="INotification"/> which in turn enables <see cref="MediatR"/> to work with the <see cref="IMediator"/>.
    ///     The most usefull usage (gained by inheritance) is the function <see cref="Enqueue"/> which allows more fluent enqueueing of the <see cref="IDomainEvent"/>.
    /// </summary>
    public abstract class DomainEvent
        : IDomainEvent
        , INotification
    {
        /// <summary>
        ///     The singleton <see cref="IDomainEventQueuer"/> used by <see cref="IDomainEvent"/>.
        /// </summary>
        /// <value>
        ///     Property <see cref="Queuer"/> represents the singleton <see cref="IDomainEventQueuer"/> used by <see cref="IDomainEvent"/>.
        /// </value>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when someone tries to set the <see cref="Queuer"/> after it has already be set.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <c>value</c> is <c>null</c>.
        /// </exception>
        public static IDomainEventQueuer Queuer
        {
            get => m_domainEventQueuer;
            set
            {
                /// We must ensure that the queue does not break state rules by setting the <see cref="m_domainEventQueuer"/> multiple times.
                if (m_domainEventQueuer != null)
                {
                    throw new InvalidOperationException($"The static {nameof(Queuer)} for the {nameof(DomainEvent)} instances already has the value {m_domainEventQueuer.GetType().Name}");
                }
                m_domainEventQueuer = value ?? throw new ArgumentNullException(nameof(value), $"The value of the {nameof(Queuer)} can not be null");
            }
        }

        /// <summary>
        ///     The private backfield used to store the return value for <see cref="Queuer"/>.
        /// </summary>
        private static IDomainEventQueuer m_domainEventQueuer;

        /// <summary>
        ///     Enques the current instance into the <see cref="Queuer"/>
        /// </summary>
        public void Enqueue()
            => Queuer.Enqueue(this);
    }
}
