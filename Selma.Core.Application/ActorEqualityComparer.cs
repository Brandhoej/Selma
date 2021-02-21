using Selma.Core.Application.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Application
{
    internal class ActorEqualityComparer
        : IEqualityComparer
        , IEqualityComparer<IActor>
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

            return Equals(x as IActor, y as IActor);
        }

        public bool Equals(IActor x, IActor y)
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

            return x.Successor == null && y.Successor == null ||
                x.Successor.Equals(y.Successor);
        }

        public int GetHashCode(object obj)
            => obj.GetHashCode();

        public int GetHashCode(IActor obj)
        {
            HashCode hashCode = new HashCode();
            hashCode.Add(this);
            hashCode.Add(obj.Successor);
            return hashCode.ToHashCode();
        }
    }
}
