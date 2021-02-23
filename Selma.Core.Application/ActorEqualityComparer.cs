using Selma.Core.Application.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

            if (x.GetHashCode() != y.GetHashCode())
            {
                return false;
            }

            return x.Successor is null && y.Successor is null ||
                x is object && x.Successor.Equals(y.Successor);
        }

        public int GetHashCode(object obj)
            => obj is null ? 0 : obj.GetHashCode();

        public int GetHashCode(IActor obj)
        {
            if (obj is null)
            {
                return 0;
            }

            HashCode hashCode = new HashCode();
            hashCode.Add(RuntimeHelpers.GetHashCode(obj));
            if (obj.Successor is object)
            {
                hashCode.Add(obj.Successor);
            }
            return hashCode.ToHashCode();
        }
    }
}
