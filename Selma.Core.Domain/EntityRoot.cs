using Selma.Core.Domain.Abstractions;
using System;

namespace Selma.Core.Domain
{
    public abstract class EntityRoot 
        : EntityRoot<Guid>
    {
        protected EntityRoot()
            : this(Guid.NewGuid())
        { }

        protected EntityRoot(Guid id)
            : base(id)
        { }
    }

    public abstract class EntityRoot<TId> 
        : Entity<TId>
        , IEntityRoot<TId>
    {
        public EntityRoot(TId id)
            : base(id)
        { }

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

            return Equals((IEntity<TId>)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"EntityRoot<{Id}>";
        }

        public static bool operator ==(EntityRoot<TId> a, EntityRoot<TId> b)
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

        public static bool operator !=(EntityRoot<TId> a, EntityRoot<TId> b)
        {
            return !(a == b);
        }
    }
}
