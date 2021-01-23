using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Domain.Events.Abstractions
{
    public interface IDomainEventQueue : IDomainEventQueuer, IEnumerable<IDomainEvent>, IEnumerable, IReadOnlyCollection<IDomainEvent>, ICollection
    {
        new int Count { get; }

        IDomainEvent Dequeue();
    }
}
