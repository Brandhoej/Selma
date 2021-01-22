using Selma.Core.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Selma.Core.Domain
{
    public abstract class ValueObject 
        : IDomainObject
        , IValueObject
        , IEquatable<ValueObject>
    {
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

            return Equals((ValueObject)obj);
        }

        public bool Equals( ValueObject other)
        {
            return GetEqualityComponent().SequenceEqual(other.GetEqualityComponent());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponent()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                }
            );
        }

        protected abstract IEnumerable<object> GetEqualityComponent();

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
