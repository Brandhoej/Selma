using Selma.Core.Domain.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selma.Core.Domain
{
    /// <inheritdoc cref="IEntity"/>
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

    /// <inheritdoc cref="IEntity{TId}"/>
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
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Entity<TId>)obj);
        }

        public bool Equals(IEntity<TId> other)
        {
            return other != null && other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Entity<{Id}>";
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (a is null && b is null)
            {
                return false;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }
    }
}
