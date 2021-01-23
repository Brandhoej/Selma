namespace Selma.Core.Application.Abstractions
{
    /// <summary>
    ///     Represents the input of a <see cref="IUseCase{TRequest, TResponse}"/> 
    ///     with <typeparamref name="TResponse"/> as the return 
    ///     type of the <see cref="IUseCase{TRequest, TResponse}"/>
    /// </summary>
    /// <typeparam name="TResponse">
    ///     The type for the <see cref="IUseCase{TRequest, TResponse}"/>
    ///     to return if this type of <see cref="IUseCaseRequest{TResponse}"/>
    ///     has been requested and executed.
    /// </typeparam>
    public interface IUseCaseRequest<TResponse>
    { }
}
