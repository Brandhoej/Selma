using MediatR;
using Selma.Core.Domain.Events.Abstractions;
using Selma.Core.MessageQueue.Abstractions;
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
        ///     The singleton <see cref="IMessageQueueProducer<IDomainEvent>"/> used by <see cref="IDomainEvent"/>.
        /// </summary>
        /// <value>
        ///     Property <see cref="Producer"/> represents the singleton <see cref="IMessageQueueProducer<IDomainEvent>"/> used by <see cref="IDomainEvent"/>.
        /// </value>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when someone tries to set the <see cref="Producer"/> after it has already be set.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <c>value</c> is <c>null</c>.
        /// </exception>
        public static IMessageQueueProducer<IDomainEvent> Producer
        {
            get => m_producer;
            set
            {
                /// We must ensure that the queue does not break state rules by setting the <see cref="m_producer"/> multiple times.
                if (m_producer != null)
                {
                    throw new InvalidOperationException($"The static {nameof(Producer)} for the {nameof(DomainEvent)} instances already has the value {m_producer.GetType().Name}");
                }
                m_producer = value ?? throw new ArgumentNullException(nameof(value), $"The value of the {nameof(Producer)} can not be null");
            }
        }

        /// <summary>
        ///     The private backfield used to store the return value for <see cref="Producer"/>.
        /// </summary>
        private static IMessageQueueProducer<IDomainEvent> m_producer;

        /// <summary>
        ///     Enques the current instance into the <see cref="Producer"/>
        /// </summary>
        public void Enqueue()
            => Producer.Enqueue(this);

        public override bool Equals(object obj)
            => new DomainEventEqualityComparer().Equals(this, obj);

        public bool Equals(IDomainEvent other)
            => new DomainEventEqualityComparer().Equals(this, other);

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator !=(DomainEvent left, IDomainEvent right)
            => !(left == right);

        public static bool operator ==(DomainEvent left, IDomainEvent right)
        {
            if (left is null && right is null)
            {
                return false;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}
