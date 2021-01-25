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
    public class UseCaseRequest<TResponse>
        : IUseCaseRequest<TResponse>
        , IRequest<TResponse>
        where TResponse
        : class
    { }
}
