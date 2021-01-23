using Selma.Core.Domain.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Selma.Core.Domain
{
    public abstract class Entity 
        : Entity<Guid>
    {
        protected Entity()
            : this(Guid.NewGuid())
        { }

        public Entity(Guid id)
            : base(id)
        { }
    }

    public abstract class Entity<TId> 
        : IEntity<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; private set; }

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

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }
    }
}
