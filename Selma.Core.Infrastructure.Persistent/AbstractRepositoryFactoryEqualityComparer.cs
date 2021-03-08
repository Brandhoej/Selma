using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Selma.Core.Infrastructure.Persistent
{
    internal class AbstractRepositoryFactoryEqualityComparer<TContext>
        : IEqualityComparer<IAbstractRepositoryFactory<TContext>>
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

            return Equals(x as IAbstractRepositoryFactory<TContext>, y as IAbstractRepositoryFactory<TContext>);
        }

        public bool Equals(IAbstractRepositoryFactory<TContext> x, IAbstractRepositoryFactory<TContext> y)
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
            => GetHashCode((obj ?? throw new ArgumentNullException(nameof(obj))) as IAbstractRepositoryFactory<TContext>);

        public int GetHashCode(IAbstractRepositoryFactory<TContext> obj)
            => RuntimeHelpers.GetHashCode(obj);
    }
}
