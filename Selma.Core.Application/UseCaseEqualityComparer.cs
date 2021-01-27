using MediatR;
using Selma.Core.Application.Abstractions;
using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Application
{
    internal class UseCaseEqualityComparer<TRequest, TResponse>
        : IEqualityComparer
        , IEqualityComparer<IUseCase<TRequest, TResponse>>
        where TResponse
        : class
        where TRequest
        : class
        , IUseCaseRequest<TResponse>
        , IRequest<TResponse>
    {
        public new bool Equals(object x, object y)
        {
            if (x == null)
            {
                return false;
            }

            if (y == null)
            {
                return false;
            }

            if (!x.GetType().Equals(y.GetType()))
            {
                return false;
            }

            return Equals(x as IActor, y as IActor);
        }

        public bool Equals(IUseCase<TRequest, TResponse> x, IUseCase<TRequest, TResponse> y)
        {
            if (y == null)
            {
                return false;
            }

            if (y.GetHashCode() != GetHashCode())
            {
                return false;
            }

            if (!ReferenceEquals(this, y))
            {
                return false;
            }

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
            => obj.GetHashCode();

        public int GetHashCode(IUseCase<TRequest, TResponse> obj)
            => obj.GetHashCode();
    }
}
