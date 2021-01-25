using MediatR;
using Selma.Core.Application.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Selma.Core.Application
{
    /// <summary>
    ///     Represents a <see cref="UseCase{TRequest, TResponse}"/> which 
    ///     through a <see cref="IMediator"/> can be invoked with a
    ///     <see cref="IUseCaseRequest{TResponse}"/>. 
    ///     The <see cref="UseCase{TRequest, TResponse}"/> has been setup
    ///     to work in conjunction with <see cref="IMediator"/>. This
    ///     conjunction is in place through the <see cref="IRequestHandler{TRequest, TResponse}"/>
    ///     inheritance and the <see cref="IRequest{TResponse}"/> constraint on <typeparamref name="TRequest"/>
    ///     
    ///     If the <see cref="IMediator"/> is setup correctly with <see cref="IServiceCollection"/>
    ///     then it is possible to have a parameterized constructor where one can expect the
    ///     <see cref="IServiceProvider"/> to dependency inject though the constructor.
    /// </summary>
    /// <typeparam name="TRequest">
    ///     The <see cref="IUseCaseRequest{TResponse}"/> which should be mediated by
    ///     <see cref="IMediator"/> for the <see cref="UseCase{TRequest, TResponse}"/>
    ///     to be handled.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     The response <see cref="Type"/> to return after the <see cref="UseCase{TRequest, TResponse}"/>
    ///     has been handled.
    /// </typeparam>
    public abstract class UseCase<TRequest, TResponse> 
        : IUseCase<TRequest, TResponse>
        , IRequestHandler<TRequest, TResponse>
        where TResponse
        : class
        where TRequest 
        : class
        , IUseCaseRequest<TResponse>
        , IRequest<TResponse>
    {
        /// <summary>
        ///     Handles the <see cref="UseCase{TRequest, TResponse}"/> with a 
        ///     <typeparamref name="TRequest"/> and returns a <typeparamref name="TResponse"/>.
        /// </summary>
        /// <param name="request">
        ///     The <typeparamref name="TRequest"/> object to process based upon.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="CancellationToken"/> to handle the async task.
        /// </param>
        /// <returns>
        ///     An object with the result of the <typeparamref name="TRequest"/>.
        /// </returns>
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }
}
