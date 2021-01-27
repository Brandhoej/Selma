using System.Collections;
using System.Collections.Generic;
using Selma.Core.Infrastructure.Persistent.Abstractions;

namespace Selma.Core.Infrastructure.Persistent
{
    internal class UnitOfWorkEqualityComparer<TContext>
        : IEqualityComparer<IUnitOfWork<TContext>>
        , IEqualityComparer
        where TContext
        : class
        , IContext
    {
        public new bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            if (!x.GetType().Equals(y.GetType()))
            {
                return false;
            }

            return Equals(x as IUnitOfWork<TContext>, y as IUnitOfWork<TContext>);
        }

        public bool Equals(IUnitOfWork<TContext> x, IUnitOfWork<TContext> y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            if (!ReferenceEquals(x, y))
            {
                return false;
            }

            if (GetHashCode(x) == GetHashCode(y))
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(object obj)
            => GetHashCode(obj as IUnitOfWork<TContext>);

        public int GetHashCode(IUnitOfWork<TContext> obj)
            => obj.GetHashCode();
    }
}
