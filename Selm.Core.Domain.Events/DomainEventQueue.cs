using Selma.Core.Domain.Events.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Selma.Core.Domain.Events
{
    /// <summary>
    ///     Represents a kind of <see cref="Queue"/> like structure used for <see cref="IDomainEvent"/>.
    ///     The implementation internally uses a <see cref="Queue"/> to store <see cref="IDomainEvent"/>.
    /// </summary>
    public class DomainEventQueue 
        : IDomainEventQueue
    {
        /// <summary>
        ///     The <see cref="Queue{IDomainEvent}"/> used to store <see cref="IDomainEvent"/> and keep order in dispatching/publishing.
        /// </summary>
        private readonly Queue<IDomainEvent> m_domainEvents;

        /// <summary>
        ///     Initialize a <see cref="DomainEventQueue"/> without initial <see cref="IDomainEvent"/>.
        /// </summary>
        public DomainEventQueue()
            : this(Enumerable.Empty<IDomainEvent>())
        { }

        /// <summary>
        ///     Initialize a <see cref="DomainEventQueue"/> with initial <see cref="IDomainEvent"/> stored in a <see cref="IEnumerable{IDomainEvent}"/>.
        /// </summary>
        /// <param name="domainEvents">
        ///     The initial <see cref="IDomainEvent"/> to store in the <see cref="DomainEventQueue"/> upon initilization.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="domainEvents"/> is null.
        /// </exception>
        public DomainEventQueue(IEnumerable<IDomainEvent> domainEvents)
            => m_domainEvents = new Queue<IDomainEvent>(domainEvents ?? throw new ArgumentNullException(nameof(domainEvents)));

        /// <summary>
        ///     Gets the number of <see cref="IDomainEvent"/> contained in the <see cref="DomainEventQueue"/>
        /// </summary>
        public int Count => m_domainEvents.Count;

        /// <summary>
        ///     Gets a value indicating whether access to the <see cref="ICollection"/> is synchronized (thread safe).
        ///     Returns true if access to the <see cref="ICollection"/> is synchronized (thread safe); otherwise, false.
        /// </summary>
        public bool IsSynchronized => (m_domainEvents as ICollection).IsSynchronized;

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the <see cref="ICollection"/>.
        ///     An object that can be used to synchronize access to the <see cref="ICollection"/>.
        /// </summary>
        public object SyncRoot => (m_domainEvents as ICollection).SyncRoot;

        /// <summary>
        ///     Copies the queued <see cref="IDomainEvent"/> elements to an existing one-dimensional
        ///     <see cref="Array"/>, starting at the specified array index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="Array"/> 
        ///     that is the destination of the elements copied. 
        ///     The <see cref="Array"/> must have a zero-based indexing and the contained type must be <see cref="IDomainEvent"/>
        /// </param>
        /// <param name="index">
        ///     The zero-based index in array at which copying begins.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="index"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The number of elements from this source to copy is greater than the available space from <paramref name="index"/>
        ///     to the end of <paramref name="array"/>.
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///     The <paramref name="array"/> does not contain <see cref="IDomainEvent"/> element type.
        /// </exception>
        public void CopyTo(Array array, int index)
            => m_domainEvents.CopyTo(array as IDomainEvent[], index);

        /// <summary>
        ///     Removes and returns the <see cref="IDomainEvent"/> at the beginning.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDomainEvent"/> that is removed from the beginning.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="Count"/> of this <see cref="DomainEventQueue"/> is 0.
        /// </exception>
        public IDomainEvent Dequeue()
            => m_domainEvents.Dequeue();

        /// <summary>
        ///     Adds the <paramref name="domainEvent"/> to the end of this <see cref="DomainEventQueue"/>.
        /// </summary>
        /// <param name="domainEvent">
        ///     The <see cref="IDomainEvent"/> to add to the <see cref="DomainEventQueue"/>.
        ///     The <paramref name="domainEvent"/> can not be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="domainEvent"/> is null.
        /// </exception>
        public void Enqueue(IDomainEvent domainEvent)
            => m_domainEvents.Enqueue(domainEvent ?? throw new ArgumentNullException(nameof(domainEvent)));

        /// <summary>
        ///     Returns an enumerator that iterates through the <see cref="DomainEventQueue"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerator{IDomainEvent}"/> for the <see cref="DomainEventQueue"/>.
        /// </returns>
        public IEnumerator<IDomainEvent> GetEnumerator()
            => m_domainEvents.GetEnumerator();

        /// <summary>
        ///     Returns an enumerator that iterates thorugh the <see cref="ICollection"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerator"/> object that can be used to iterate though the <see cref="ICollection"/>
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
            => (m_domainEvents as IEnumerable).GetEnumerator();
    }
}
