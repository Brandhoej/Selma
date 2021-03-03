using Selma.Core.Domain.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selma.Core.Domain
{
    /// <summary>
    ///     <inheritdoc cref="IEntity"/>
    /// </summary>
    public abstract class Entity 
        : Entity<Guid>
    {
        /// <summary>
        ///     Initializes an <see cref="Entity"/> with a <see cref="Guid.NewGuid"/> 
        ///     as the used Id for the <see cref="IIdentifiableDomainObject{TId}.Id"/>.
        /// </summary>
        protected Entity()
            : this(Guid.NewGuid())
        { }

        /// <summary>
        ///     <inheritdoc cref="Entity{TId}.Entity(TId)"/>
        /// </summary>
        /// <param name="id">
        ///     <inheritdoc cref="Entity{TId}.Entity(TId)"/>
        /// </param>
        public Entity(Guid id)
            : base(id)
        { }
    }

    /// <summary>
    ///     <inheritdoc cref="IEntity{TId}"/>
    /// </summary>
    /// <typeparam name="TId">
    ///     <inheritdoc cref="IEntity{TId}"/>
    /// </typeparam>
    public abstract class Entity<TId> 
        : IEntity<TId>
    {
        /// <summary>
        ///     The unique id of the instanc with the type <typeparamref name="TId"/>.
        ///     <list>
        ///         <item>
        ///             <see cref="KeyAttribute"/>
        ///             <description>
        ///                 Used to denote that the <see cref="Entity{TId}.Id"/> is the unique identity.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <see cref="DatabaseGeneratedAttribute"/>
        ///             <description>
        ///                 Since the <typeparamref name="TId"/> is not necessarily an <see cref="int"/>
        ///                 then we have to enform the database to generate id explicitly.
        ///             </description>
        ///         </item>
        ///     </list>
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; private set; }

        /// <summary>
        ///     Initializes an <see cref="Entity{TId}"/> with the Id of <typeparamref name="TId"/>.
        /// </summary>
        /// <param name="id">
        ///     The id of the <see cref="Entity{TId}"/> as stored in the <see cref="IIdentifiableDomainObject{TId}"/>.
        /// </param>
        public Entity(TId id)
            => Id = id;

        /// <summary>
        ///     Returns a value indicating whether this <see cref="IEntity{TId}"/> 
        ///     is equal to a specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">
        ///     A <see cref="object"/> to compare with this 
        ///     instance of <see cref="IEntity{TId}"/>.
        /// </param>
        /// <returns>
        ///     True if <see cref="Equals(IEntity{TId})"/> returns true;
        ///     otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
            => new EntityEqualityComparer<TId>().Equals(this, obj);

        /// <summary>
        ///     Returns a value indicating whether this <see cref="IEntity{TId}"/> 
        ///     is equal to a specified <paramref name="other"/>
        /// </summary>
        /// <param name="other">
        ///     A <see cref="IEntity{TId}"/> to compare with this 
        ///     instance of <see cref="IEntity{TId}"/>.
        /// </param>
        /// <returns>
        ///     True if <see cref="Equals(IEntity{TId})"/> returns true;
        ///     otherwise, false.
        /// </returns>
        public bool Equals(IEntity<TId> other)
            => new EntityEqualityComparer<TId>().Equals(this, other);

        /// <summary>
        ///     Returns the <see cref="int"/> hash code for the <see cref="Entity{TId}"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="int"/> hash code.
        /// </returns>
        public override int GetHashCode()
            => new EntityEqualityComparer<TId>().GetHashCode(this);

        /// <summary>
        ///     Inequality operator implementation of the <see cref="Entity{TId}"/>.
        /// </summary>
        /// <param name="left">
        ///     Left side of the inequality operator.
        /// </param>
        /// <param name="right">
        ///     Right side of the inequality operator.
        /// </param>
        /// <returns>
        ///     False if <paramref name="left"/> is equal to <paramref name="right"/>;
        ///     otherwise, true.
        /// </returns>
        public static bool operator !=(Entity<TId> left, IEntity<TId> right)
            => !(left == right);

        /// <summary>
        ///     Equality operator implementation of the <see cref="Entity{TId}"/>.
        /// </summary>
        /// <param name="left">
        ///     Left side of the equality operator.
        /// </param>
        /// <param name="right">
        ///     Right side of the equality operator.
        /// </param>
        /// <returns>
        ///     True if <paramref name="left"/> is equal to <paramref name="right"/>;
        ///     otherwise, false.
        /// </returns>
        public static bool operator ==(Entity<TId> left, IEntity<TId> right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}
