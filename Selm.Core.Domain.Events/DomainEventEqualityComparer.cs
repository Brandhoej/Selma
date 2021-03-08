using Selma.Core.Domain.Events.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Selma.Core.Domain.Events
{
    internal class DomainEventEqualityComparer
        : IEqualityComparer<IDomainEvent>
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

            return Equals(x as IDomainEvent, y as IDomainEvent);
        }

        public bool Equals(IDomainEvent x, IDomainEvent y)
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

            return true;
        }

        public int GetHashCode(object obj)
            => obj is null ? 0 : GetHashCode(obj as IDomainEvent);

        public int GetHashCode(IDomainEvent obj)
            => obj is null ? 0 : RuntimeHelpers.GetHashCode(obj);
    }
}
