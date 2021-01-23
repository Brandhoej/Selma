using Selma.Core.Domain.Abstractions;

namespace Selma.Core.Domain.Events.Abstractions
{
    public interface IDomainEvent : IDomainObject
    {
        void Enqueue();
    }
}
