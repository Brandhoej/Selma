using MediatR;
using Selma.Core.Application.Abstractions;

namespace Selma.Core.Application
{
    public class UseCaseRequest<TResponse>
        : IUseCaseRequest<TResponse>
        , IRequest<TResponse>
    { }
}
