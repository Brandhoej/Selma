using Selma.Core.Application.Abstractions;
using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Application
{
    internal class UseCaseRequestEqualityComparer<TResponse>
        : IEqualityComparer
        , IEqualityComparer<IUseCaseRequest<TResponse>>
        where TResponse
        : class
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

        public bool Equals(IUseCaseRequest<TResponse> x, IUseCaseRequest<TResponse> y)
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

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
            => obj.GetHashCode();

        public int GetHashCode(IUseCaseRequest<TResponse> obj)
            => obj.GetHashCode();
    }
}
