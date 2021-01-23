namespace Selma.Core.Application.Abstractions
{
    public interface IUseCase<TRequest, TResponse>
        where TRequest : IUseCaseRequest<TResponse>
    { }
}
