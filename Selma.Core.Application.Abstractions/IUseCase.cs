using System;

namespace Selma.Core.Application.Abstractions
{
    /// <summary>
    ///     Represents a use case in a domain.
    ///     The <see cref="IUseCase{TRequest, TResponse}"/> is condifgured with a
    ///     <typeparamref name="TRequest"/> and <typeparamref name="TResponse"/>
    ///     this is done so that we can use type specific information
    ///     for a mediator class.
    /// </summary>
    /// <typeparam name="TRequest">
    ///     The request the <see cref="IUseCase{TRequest, TResponse}"/> 
    ///     uses for handling the execution of the task.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     The respose for the <see cref="IUseCase{TRequest, TResponse}"/>
    ///     to return after the execution of the task.
    /// </typeparam>
    public interface IUseCase<TRequest, TResponse>
        : IEquatable<IUseCase<TRequest, TResponse>>
        where TResponse
        : class
        where TRequest
        : class
        , IUseCaseRequest<TResponse>
    { }
}
