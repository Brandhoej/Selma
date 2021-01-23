using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Application.Abstractions
{
    public interface IActor
    {
        IActor Successor { get; }

        ValueTask<TResponse> Do<TResponse>(IUseCaseRequest<TResponse> request, CancellationToken cancellationToken = default);

        IEnumerable<Assembly> GetSupportedAssemblies();
        IEnumerable<Type> GetSupportedUseCases();

        bool SupportsUseCase<TResponse>();
    }
}
