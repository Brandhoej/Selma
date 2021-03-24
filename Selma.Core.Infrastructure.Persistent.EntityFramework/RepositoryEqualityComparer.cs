using Selma.Core.Domain.Abstractions;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Selma.Core.Infrastructure.Persistent.EntityFramework
{
    internal class RepositoryEqualityComparer<TEntity, TId>
        : IEqualityComparer<IRepository<TEntity, TId>>
        , IEqualityComparer
        where TEntity
        : class
        , IEntityRoot<TId>
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

            return Equals(x as IRepository<TEntity, TId>, y as IRepository<TEntity, TId>);

        }

        public bool Equals(IRepository<TEntity, TId> x, IRepository<TEntity, TId> y)
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
            => obj is null ? 0 : GetHashCode(obj as IRepository<TEntity, TId>);

        public int GetHashCode(IRepository<TEntity, TId> obj)
            => obj is null ? 0 : RuntimeHelpers.GetHashCode(obj);
    }
}
