using MediatR;
using Selma.Core.Application.Abstractions;
using System;

namespace Selma.Core.Application
{
    /// <summary>
    ///     Represents a <see cref="IUseCaseRequest{TResponse}"/>
    ///     which are supported with <see cref="IMediator"/> from the 
    ///     inheritance of <see cref="IRequest{TResponse}"/>
    /// </summary>
    /// <typeparam name="TResponse">
    ///     The <see cref="Type"/> to return from the <see cref="IUseCaseRequest{TResponse}"/>
    ///     after being handled.
    /// </typeparam>
    public abstract class UseCaseRequest<TResponse>
        : IUseCaseRequest<TResponse>
        , IRequest<TResponse>
        where TResponse
        : class
    {
        public override bool Equals(object obj)
            => new UseCaseRequestEqualityComparer<TResponse>().Equals(this, obj);

        public bool Equals(IUseCaseRequest<TResponse> other)
            => new UseCaseRequestEqualityComparer<TResponse>().Equals(this, other);

        public override int GetHashCode()
            => new UseCaseRequestEqualityComparer<TResponse>().GetHashCode(this);

        public override string ToString()
            => base.ToString();

        public static bool operator !=(UseCaseRequest<TResponse> left, UseCaseRequest<TResponse> right)
            => !(left == right);

        public static bool operator ==(UseCaseRequest<TResponse> left, UseCaseRequest<TResponse> right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left is null)
            {
                return false;
            }

            if (left is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}
