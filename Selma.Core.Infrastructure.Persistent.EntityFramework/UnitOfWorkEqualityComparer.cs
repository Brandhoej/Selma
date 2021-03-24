using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Selma.Core.Infrastructure.Persistent.EntityFramework
{
    internal class UnitOfWorkEqualityComparer
        : IEqualityComparer<IUnitOfWork>
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
            => obj is null ? 0 : GetHashCode(obj as IUnitOfWork);

        public int GetHashCode(IUnitOfWork obj)
            => obj is null ? 0 : RuntimeHelpers.GetHashCode(obj);
    }
}
