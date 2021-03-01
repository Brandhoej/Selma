using MediatR;
using Selma.Core.Domain.Events.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Domain.Events
{
    /// <summary>
    ///     <inheritdoc cref="IDomainEventHandler{T}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     <inheritdoc cref="IDomainEventHandler{T}"/>
    /// </typeparam>
    public abstract class DomainEventHandler<T>
        : IDomainEventHandler<T>
        , INotificationHandler<T>
        where T 
        : class
        , IDomainEvent
        , INotification
    {
        public bool Equals(IDomainEventHandler<T> other)
            => new DomainEventHandlerEqualityComparer<T>().Equals(this, other);

        public override bool Equals(object obj)
            => new DomainEventHandlerEqualityComparer<T>().Equals(this, obj);

        public override int GetHashCode()
            => new DomainEventHandlerEqualityComparer<T>().GetHashCode(this);

        /// <summary>
        ///     <inheritdoc cref="IDomainEventHandler{T}"/>
        /// </summary>
        /// <param name="notification">
        ///     <inheritdoc cref="IDomainEventHandler{T}"/>
        /// </param>
        /// <param name="cancellationToken">
        ///     <inheritdoc cref="IDomainEventHandler{T}"/>
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="IDomainEventHandler{T}"/>
        /// </returns>
        public abstract Task Handle(T notification, CancellationToken cancellationToken = default);

        public static bool operator !=(DomainEventHandler<T> left, IDomainEventHandler<T> right)
            => !(left == right);

        public static bool operator ==(DomainEventHandler<T> left, IDomainEventHandler<T> right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}
