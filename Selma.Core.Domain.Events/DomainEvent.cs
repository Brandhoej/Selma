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
        ///     The singleton <see cref="IMessageQueueProducer<IDomainEvent>"/> used by <see cref="IDomainEvent"/> if non were given in the constructor.
        /// </summary>
        /// <value>
        ///     Property <see cref="StandardProducer"/> represents the singleton <see cref="IMessageQueueProducer<IDomainEvent>"/> used by <see cref="IDomainEvent"/>.
        /// </value>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when someone tries to set the <see cref="StandardProducer"/> after it has already be set.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <c>value</c> is <c>null</c>.
        /// </exception>
        public static IMessageQueueProducer<IDomainEvent> StandardProducer
        {
            get => m_standardProducer;
            set
            {
                /// We must ensure that the queue does not break state rules by setting the <see cref="m_standardProducer"/> multiple times.
                if (m_standardProducer != null)
                {
                    throw new InvalidOperationException($"The static {nameof(StandardProducer)} for the {nameof(DomainEvent)} instances already has the value {m_standardProducer.GetType().Name}");
                }
                m_standardProducer = value ?? throw new ArgumentNullException(nameof(value), $"The value of the {nameof(StandardProducer)} can not be null");
            }
        }

        /// <summary>
        ///     The private backfield used to store the return value for <see cref="StandardProducer"/>.
        /// </summary>
        private static IMessageQueueProducer<IDomainEvent> m_standardProducer;

        /// <summary>
        ///     Constructs a <see cref="DomainEvent"/> with the <see cref="StandardProducer"/> as the producer used to handle its enqueue
        /// </summary>
        public DomainEvent()
            : this(StandardProducer)
        { }

        /// <summary>
        ///     Constructs a <see cref="DomainEvent"/> with a specific <see cref="IMessageQueueProducer{TMessage}"/> given by the <paramref name="producer"/> parameter.
        /// </summary>
        /// <param name="producer">
        ///     The <see cref="IMessageQueueProducer{TMessage}"/> used to enqueue this event.
        /// </param>
        public DomainEvent(IMessageQueueProducer<IDomainEvent> producer)
            => Producer = producer ?? throw new ArgumentNullException(nameof(producer));

        /// <summary>
        ///     The <see cref="IMessageQueueProducer{TMessage}"/> used to handle this <see cref="DomainEvent"/> enqueueing.
        /// </summary>
        private IMessageQueueProducer<IDomainEvent> Producer { get; }

        /// <summary>
        ///     Enques the current instance into the <see cref="StandardProducer"/>
        /// </summary>
        public void Enqueue()
            => Producer.Enqueue(this);

        public override bool Equals(object obj)
            => new DomainEventEqualityComparer().Equals(this, obj);

        public bool Equals(IDomainEvent other)
            => new DomainEventEqualityComparer().Equals(this, other);

        public override int GetHashCode()
            => new DomainEventEqualityComparer().GetHashCode(this);

        public static bool operator !=(DomainEvent left, IDomainEvent right)
            => !(left == right);

        public static bool operator ==(DomainEvent left, IDomainEvent right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}
