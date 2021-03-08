using Selma.Core.Application.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

            return Equals(x as IUseCaseRequest<TResponse>, y as IUseCaseRequest<TResponse>);
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

            if (!ReferenceEquals(x, y))
            {
                return false;
            }

            if (x.GetHashCode() != y.GetHashCode())
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(object obj)
            => obj is null ? 0 : GetHashCode(obj as IUseCaseRequest<TResponse>);

        public int GetHashCode(IUseCaseRequest<TResponse> obj)
            => obj is null ? 0 : RuntimeHelpers.GetHashCode(obj);
    }
}
