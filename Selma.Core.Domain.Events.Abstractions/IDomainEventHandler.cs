using System;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Domain.Events.Abstractions
{
    /// <summary>
    ///     Defines a handler for a <see cref="IDomainEvent"/> defined by <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    ///     The <see cref="IDomainEvent"/> to handle.
    /// </typeparam>
    public interface IDomainEventHandler<T>
        : IEquatable<IDomainEventHandler<T>>
        where T 
        : class
        , IDomainEvent
    {
        /// <summary>
        ///     Handles a <see cref="IDomainEvent"/> notification as defined by <typeparamref name="T"/>.
        /// </summary>
        /// <param name="notification">
        ///     The <see cref="IDomainEvent"/> which was invoked.
        /// </param>
        /// <param name="cancellationToken">
        ///     The task cancellation used to propagate that the operation should be canceled.
        /// </param>
        /// <returns>
        ///     The async task handler for the handling of the <paramref name="notification"/>.
        /// </returns>
        Task Handle(T notification, CancellationToken cancellationToken);
    }
}
