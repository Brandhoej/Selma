using System;

namespace Selma.Core.Domain.Abstractions
{
    public interface IEntity 
        : IEntity<Guid>
    { }

    public interface IEntity<TId> 
        : IIdentifiableDomainObject<TId>
        , IEquatable<IEntity<TId>>
    { }
}
