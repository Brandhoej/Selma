using System.Threading.Tasks;

namespace Selma.Core.Domain.Events.Abstractions
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
