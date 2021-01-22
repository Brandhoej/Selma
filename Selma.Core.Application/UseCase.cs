using MediatR;
using Selma.Core.Application.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Application
{
    public abstract class UseCase<TRequest, TResponse> 
        : IUseCase<TRequest, TResponse>
        , IRequestHandler<TRequest, TResponse>
        where TRequest 
        : IUseCaseRequest<TResponse>
        , IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }
}
