using System;

namespace Selma.Core.Domain.Abstractions
{
    /// <summary>
    ///     This <see cref="IEntity{TId}"/> implementation uses the <see cref="Guid"/> as Id <see cref="Type"/>.
    ///     This is a standard enforced by msdn (ex. their eShopOnContainers, see <see href="https://github.com/dotnet-architecture/eShopOnContainers"/>).
    ///     However, UUIDs implies a security risk because of their layout containing a lot of time predictable information
    ///     therefore remember to enforce security when using such an Id.
    /// </summary>
    /// <remarks>
    ///     <inheritdoc cref="IEntity{TId}"/>
    /// </remarks>
    /// <example>
    ///     <inheritdoc cref="IEntity{TId}"/>
    /// </example>
    public interface IEntity 
        : IEntity<Guid>
    { }

    /// <summary>
    ///     This <see cref="IEntityRoot{TId}"/> allows you to choose the <typeparamref name="TId"/> 
    ///     determining the ID type of the <see cref="IIdentifiableDomainObject{TId}"/>
    /// </summary>
    /// <remarks>
    ///     Represents an entity following the definition of domain driven design:
    ///     An object that is not defined by its attributes, but rather by a thread of continuity and its identity.
    /// </remarks>
    /// <example>
    ///     Most airlines distinguish each seat uniquely on every flight.
    ///     Each seat is an <see cref="IEntity"/> in this context. However, Southwest Airlines,
    ///     EasyJet and Ryanairdo not distinguish between every seat; all seats are the same.
    ///     In this context, a seat is actually a <see cref="IValueObject"/>.
    /// </example>
    /// <typeparam name="TId">
    ///     The <see cref="Type"/> to use for the <see cref="IIdentifiableDomainObject{TId}"/> type determining the Id of the instance.
    /// </typeparam>
    public interface IEntity<TId> 
        : IIdentifiableDomainObject<TId>
        , IEquatable<IEntity<TId>>
    { }
}
