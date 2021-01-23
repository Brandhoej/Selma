using System.Threading.Tasks;

namespace Selma.Core.Domain.Events.Abstractions
{
    public interface IDeferredDomainEventDispatcher
        : IDomainEventDispatcher
    {
        Task DispatchAll();
    }
}
