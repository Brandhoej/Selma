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

        /// <inheritdoc cref="Entity{TId}.Equals(object)"/>
        public override bool Equals(object obj)
            => base.Equals(obj);

        /// <inheritdoc cref="Entity{TId}.Equals(IEntity{TId})"/>
        public bool Equals(IEntityRoot<TId> other)
            => base.Equals(other);

        /// <inheritdoc cref=" Entity{TId}.GetHashCode"/>
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
        ///     Left side of the inequality operator.
        /// </param>
        /// <param name="right">
        ///     Right side of the inequality operator.
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
        ///     Left side of the equality operator.
        /// </param>
        /// <param name="right">
        ///     Right side of the equality operator.
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
