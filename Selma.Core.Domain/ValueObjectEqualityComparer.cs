using Selma.Core.Domain.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Domain
{
    internal class ValueObjectEqualityComparer
        : IEqualityComparer<IValueObject>
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

            return Equals(x as IValueObject, y as IValueObject);
        }

        public bool Equals(IValueObject x, IValueObject y)
        {
            if (x is null)
            {
                return false;
            }

            if (y is null)
            {
                return false;
            }

            if (!ReferenceEquals(this, y))
            {
                return false;
            }

            if (y.GetHashCode() != GetHashCode())
            {
                return false;
            }

            return true;
        }


        public int GetHashCode(object obj)
            => GetHashCode(obj as IValueObject);

        public int GetHashCode(IValueObject obj)
            => obj.GetHashCode();
    }
}
