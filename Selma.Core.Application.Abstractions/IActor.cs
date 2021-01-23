using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Application.Abstractions
{
    /// <summary>
    ///     represents an <see cref="IActor"/> in a domain.
    ///     The <see cref="IActor"/> can be configured by <see cref="IUseCase{TRequest, TResponse}"/>
    ///     through <see cref="IActor.GetSupportedAssemblies"/> and <see cref="IActor.GetSupportedUseCases"/>.
    ///     The <see cref="IActor"/> does not support the usage of MediatR out of the box.
    /// </summary>
    public interface IActor
    {
        /// <summary>
        ///     The next link in the chain of responsibility.
        /// </summary>
        IActor Successor { get; }

        /// <summary>
        ///     Used for the <see cref="IActor"/> handling of a <see cref="IUseCase{TRequest, TResponse}"/>
        ///     found through the type of the <see cref="IUseCaseRequest{TResponse}"/>.
        /// </summary>
        /// <typeparam name="TResponse">
        ///     The return type of the <see cref="IUseCaseRequest{TResponse}"/> which 
        ///     should be handled.
        /// </typeparam>
        /// <param name="request">
        ///     The object containing the data for handling the <see cref="IUseCase{TRequest, TResponse}"/>.
        /// </param>
        /// <param name="cancellationToken">
        ///     Optional cancellation token for the <see cref="IUseCase{TRequest, TResponse}"/> operation.
        /// </param>
        /// <returns>
        ///     The <see cref="ValueTask{TResponse}"/> wrapper around the <see cref="Task"/>. 
        ///     The result type of which is <typeparamref name="TResponse"/>.
        /// </returns>
        ValueTask<TResponse> Do<TResponse>(IUseCaseRequest<TResponse> request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Checks whether the <see cref="IActor"/> supports the <see cref="IUseCase{IUseCaseRequest{TResponse}, TResponse}"/> 
        ///     with the response of type <typeparamref name="TResponse"/>.
        /// </summary>
        /// <typeparam name="TResponse">
        ///     The type to check for a supported <see cref="IUseCase{TRequest, TResponse}"/>.
        /// </typeparam>
        /// <returns>
        ///     True if the <see cref="IActor"/> supports the <see cref="IUseCase{TRequest, TResponse}"/> 
        ///     with the return type <typeparamref name="TResponse"/>; otherwise, false.
        /// </returns>
        bool SupportsUseCase<TResponse>();

        /// <summary>
        ///     Enables other <see cref="IActor"/> specialization classes to define which <see cref="Enumerable.Empty{Assembly}"/> 
        ///     with <see cref="IUseCase{TRequest, TResponse}"/> the <see cref="IActor"/> supports.
        /// </summary>
        /// <returns>
        ///     By default <see cref="Enumerable.Empty{Assembly}"/>.
        /// </returns>
        IEnumerable<Assembly> GetSupportedAssemblies();

        /// <summary>
        ///     Enables other <see cref="IActor"/> specialization classes to define which <see cref="Enumerable.Empty{Assembly}"/> 
        ///     with <see cref="IUseCase{TRequest, TResponse}"/> the <see cref="IActor"/> supports.
        /// </summary>
        /// <returns>
        ///     By default <see cref="Enumerable.Empty{Assembly}"/>.
        /// </returns>
        IEnumerable<Type> GetSupportedUseCases();
    }
}
