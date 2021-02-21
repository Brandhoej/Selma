using Selma.Core.Infrastructure.Persistent.Abstractions;
using System.Collections.Generic;
using System.Collections;

namespace Selma.Core.Infrastructure.Persistent.EntityFramework
{
    internal class ContextEqualityComparer
        : IEqualityComparer<IContext>
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

            return Equals(x as IContext, y as IContext);

        }

        public bool Equals(IContext x, IContext y)
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
            => GetHashCode(obj as IContext);

        public int GetHashCode(IContext obj)
            => obj.GetHashCode();
    }
}
