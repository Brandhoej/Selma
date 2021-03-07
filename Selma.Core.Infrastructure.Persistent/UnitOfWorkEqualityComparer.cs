using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Selma.Core.Infrastructure.Persistent.Abstractions;

namespace Selma.Core.Infrastructure.Persistent
{
    internal class UnitOfWorkEqualityComparer<TContext>
        : IEqualityComparer<IUnitOfWork>
        , IEqualityComparer
        where TContext
        : class
        , IContext
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

            return Equals(x as IUnitOfWork, y as IUnitOfWork);
        }

        public bool Equals(IUnitOfWork x, IUnitOfWork y)
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
            => GetHashCode(obj as IUnitOfWork);

        public int GetHashCode(IUnitOfWork obj)
            => RuntimeHelpers.GetHashCode(obj);
    }
}
