using System;

namespace Selma.Core.Domain.Abstractions
{
    public interface IEntityRoot 
        : IEntityRoot<Guid>
    { }

    public interface IEntityRoot<TId> 
        : IEntity<TId>
    { }
}
