using Selma.Core.Domain.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Domain
{
    internal class EntityEqualityComparer<TId>
        : IEqualityComparer<IEntity<TId>>
        , IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            if (x is null)
            {
                return false;
            }

            if (y is null)
            {
                return false;
            }

            if (!x.GetType().Equals(y.GetType()))
            {
                return false;
            }

            return Equals(x as IEntity<TId>, y as IEntity<TId>);
        }

        public bool Equals(IEntity<TId> x, IEntity<TId> y)
        {
            if (x is null)
            {
                return false;
            }

            if (y is null)
            {
                return false;
            }

            if (!ReferenceEquals(x, y))
            {
                return false;
            }

            if (GetHashCode(x) != GetHashCode(y))
            {
                return false;
            }

            return y.Id.Equals(x.Id);
        }

        public int GetHashCode(object obj)
            => obj is null ? 0 : GetHashCode(obj as IEntity<TId>);

        public int GetHashCode(IEntity<TId> obj)
        {
            if (obj is null)
            {
                return 0;
            }

            HashCode hashCode = new HashCode();
            hashCode.Add(obj.Id);
            hashCode.Add(typeof(TId));
            return hashCode.ToHashCode();
        }
    }
}
