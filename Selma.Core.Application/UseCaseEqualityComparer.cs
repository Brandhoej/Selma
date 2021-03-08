using MediatR;
using Selma.Core.Application.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

            return Equals(x as IUseCase<TRequest, TResponse>, y as IUseCase<TRequest, TResponse>);
        }

        public bool Equals(IUseCase<TRequest, TResponse> x, IUseCase<TRequest, TResponse> y)
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
            => obj is null ? 0 : GetHashCode(obj as IUseCase<TRequest, TResponse>);

        public int GetHashCode(IUseCase<TRequest, TResponse> obj)
            => obj is null ? 0 : RuntimeHelpers.GetHashCode(obj);
    }
}
