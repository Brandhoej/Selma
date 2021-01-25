using System;

namespace Selma.Core.Domain.Abstractions
{
    /// <summary>
    ///     An implementation of the <see cref="IIdentifiableDomainObject{TId}"/> with
    ///     <see cref="Guid"/> as the identifyer.
    /// </summary>
    public interface IIdentifiableDomainObject 
        : IIdentifiableDomainObject<Guid>
    { }

    /// <summary>
    ///     Used for <see cref="IDomainObject"/> which needs identity property
    /// </summary>
    /// <typeparam name="TId">
    ///     The type of the ID used to identify the <see cref="IDomainObject"/>.
    /// </typeparam>
    public interface IIdentifiableDomainObject<TId> 
        : IDomainObject
    {
        TId Id { get; }
    }
}
