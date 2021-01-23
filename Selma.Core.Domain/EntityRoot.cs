using Selma.Core.Domain.Abstractions;
using System;

namespace Selma.Core.Domain
{
    /// <inheritdoc cref="IEntityRoot"/>
    public abstract class EntityRoot 
        : EntityRoot<Guid>
    {
        /// <summary>
        ///     <inheritdoc cref="Entity.Entity"/>
        /// </summary>
        protected EntityRoot()
            : this(Guid.NewGuid())
        { }

        /// <summary>
        ///     <inheritdoc cref="EntityRoot{TId}.EntityRoot(TId)"/>
        /// </summary>
        /// <param name="id">
        ///     <inheritdoc cref="EntityRoot{TId}.EntityRoot(TId)"/>
        /// </param>
        protected EntityRoot(Guid id)
            : base(id)
        { }
    }

    /// <inheritdoc cref="IEntityRoot{TId}"/>
    public abstract class EntityRoot<TId> 
        : Entity<TId>
        , IEntityRoot<TId>
    {
        /// <summary>
        ///     <inheritdoc cref="Entity{TId}.Entity(TId)"/>
        /// </summary>
        /// <param name="id">
        ///     <inheritdoc cref="Entity{TId}.Entity(TId)"/>
        /// </param>
        public EntityRoot(TId id)
            : base(id)
        { }

        /// <summary>
        ///     Returns a value indicating whether this <see cref="EntityRoot{TId}"/> is equal to a specified <see cref="EntityRoot{TId}"/>.
        /// </summary>
        /// <param name="other">
        ///     A <see cref="EntityRoot{TId}"/> to compare with this instance of <see cref="EntityRoot{TId}"/>.
        /// </param>
        /// <returns>
        ///     True if <see cref="Equals(IEntityRoot{TId})"/> returns true;
        ///     otherwise, false.
        /// </returns>
        public override bool Equals(object other)
            => Equals(other);

        /// <summary>
        ///     Returns a value indicating whether this <see cref="EntityRoot{TId}"/> is equal to a specified <see cref="EntityRoot{TId}"/>.
        /// </summary>
        /// <param name="other">
        ///     A <see cref="EntityRoot{TId}"/> to compare with this instance of <see cref="EntityRoot{TId}"/>.
        /// </param>
        /// <returns>
        ///     True if <paramref name="other"/> is not null and types are the same and the <see cref="IEntity{TId}"/> are equal;
        ///     otherwise, false.
        /// </returns>
        public bool Equals(IEntityRoot<TId> other)
        {
            if (other == null)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Equals((IEntity<TId>)other);
        }

        /// <summary>
        ///     Returns the <see cref="int"/> hash code for the <see cref="EntityRoot{TId}"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="int"/> hash code.
        /// </returns>
        public override int GetHashCode()
            => base.GetHashCode();

        /// <summary>
        ///     Returns a string representation of the <see cref="EntityRoot{TId}"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="string"/> representation.
        /// </returns>
        public override string ToString()
            => $"{GetType().Name} - Root '{Id}'";

        /// <summary>
        ///     Inequality operator implementation of the <see cref="EntityRoot{TId}"/>.
        /// </summary>
        /// <param name="left">
        ///     Left side of the inequality operator
        /// </param>
        /// <param name="right">
        ///     Right side of the inequality operator
        /// </param>
        /// <returns>
        ///     False if <paramref name="left"/> is equal to <paramref name="right"/>;
        ///     otherwise, true.
        /// </returns>
        public static bool operator !=(EntityRoot<TId> left, EntityRoot<TId> right)
            => !(left == right);

        /// <summary>
        ///     Equality operator implementation of the <see cref="EntityRoot{TId}"/>.
        /// </summary>
        /// <param name="left">
        ///     Left side of the equality operator
        /// </param>
        /// <param name="right">
        ///     Right side of the equality operator
        /// </param>
        /// <returns>
        ///     True if <paramref name="left"/> is equal to <paramref name="right"/>;
        ///     otherwise, false.
        /// </returns>
        public static bool operator ==(EntityRoot<TId> left, EntityRoot<TId> right)
        {
            if (left is null && right is null)
            {
                return false;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}
