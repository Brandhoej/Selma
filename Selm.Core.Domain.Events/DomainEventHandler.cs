using MediatR;
using Selma.Core.Domain.Events.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Domain.Events
{
    /// <inheritdoc cref="IDomainEventHandler{T}"/>
    public abstract class DomainEventHandler<T>
        : IDomainEventHandler<T>
        , INotificationHandler<T>
        where T 
        : class
        , IDomainEvent
        , INotification
    {
        /// <inheritdoc cref="IDomainEventHandler{T}"/>
        public abstract Task Handle(T notification, CancellationToken cancellationToken = default);
    }
}
