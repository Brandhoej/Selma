using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Domain.Events.Abstractions
{
    public interface IDomainEventHandler<T>
        where T 
        : IDomainEvent
    {
        Task Handle(T notification, CancellationToken cancellationToken);
    }
}
