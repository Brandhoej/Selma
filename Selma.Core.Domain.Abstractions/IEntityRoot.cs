using System;

namespace Selma.Core.Domain.Abstractions
{
    /// <summary>
    ///     This <see cref="IEntityRoot{TId}"/> implementation uses the <see cref="Guid"/> as Id <see cref="Type"/>.
    ///     This is a standard enforced by msdn (ex. their eShopOnContainers, see <see href="https://github.com/dotnet-architecture/eShopOnContainers"/>).
    ///     However, UUIDs implies a security risk because of their layout containing a lot of time predictable information
    ///     therefore remember to enforce security when using such an Id.
    /// </summary>
    /// <remarks>
    ///     <inheritdoc cref="IEntityRoot{TId}"/>
    /// </remarks>
    /// <example>
    ///     <inheritdoc cref="IEntityRoot{TId}"/>
    /// </example>
    public interface IEntityRoot 
        : IEntityRoot<Guid>
    { }

    /// <summary>
    ///     This <see cref="IEntityRoot{TId}"/> allows you to choose the <typeparamref name="TId"/> 
    ///     determining the ID type of the <see cref="IIdentifiableDomainObject{TId}"/>
    /// </summary>
    /// <remarks>
    ///     Represents an aggregate root following the definition of domain driven design:
    ///     An aggregate (Not aggregate root aka. <see cref="IEntityRoot"/>) is a cluster of associated objects 
    ///     that we treat as a unit for the purpose of data changes.
    ///     Each aggregate has a root and a boundary. The boundary defines what is inside the aggregate.
    ///     The <see cref="IEntityRoot"/> is a single, specific <see cref="IEntity"/> contained in the aggregate
    ///     The <see cref="IEntityRoot"/> is the only member of the aggregate that outside objects are allowed to hold references to.
    ///     Meaning that <see cref="IEntityRoot"/> are the only objects that can be loaded from a repository.
    /// </remarks>
    /// <example>
    ///     An example is a model containing a Customer <see cref="IEntityRoot{TId}"/> and an Address <see cref="IValueObject"/>.
    ///     We would never access an Address <see cref="IValueObject"/> directly from the model as it does not
    ///     make sense without the context of an associated Customer. So we could say that Customer and 
    ///     Address together form an aggregate and that Customer is an <see cref="IEntityRoot{TId}"/>.
    /// </example>
    /// <typeparam name="TId">
    ///     The <see cref="Type"/> to use for the <see cref="IIdentifiableDomainObject{TId}"/> type determining the Id of the instance.
    /// </typeparam>
    public interface IEntityRoot<TId> 
        : IEntity<TId>
        , IEquatable<IEntityRoot<TId>>
    { }
}
