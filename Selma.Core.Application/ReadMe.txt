Dependencies:
Private:
 - Selma.Core.Application.Abstractions (9.0.0)
 - Microsoft.Extensions.DependencyInjection.Abstractions (5.0.0)
Public:
 - MediatR

Public API
 - Public interfaces:
   - IActor: Do, SupportsUseCaseWithResponse, GetSupportedAssemblies, GetSupportedUseCases.
   - IUseCase.
   - IUseCaseRequest.
 - Public classes:
   - Abstract Actor is IActor.
   - Abstract UseCase is IUseCase, IRequestHandler: Handle.
   - Abstract UseCaseRequest is IUseCaseRequest, IRequest.
   - Static ServiceCollectionExtensions: AddActor.

Features
 - Actor
   - Actor can do use cases by request object based on type.
   - Actor checking whether a use case type is supported.
   - Actor static collection of supported use cases.
   - Actor chain of responsibility by aggregation.
   - Actor caching of supported use cases.
 - UseCase
   - Accepts aynschronous handling of UseCaseRequests
 - UseCaseRequest
 - ServiceCollectionExtensions
   - WIP: Allow MediatR instantiation where the assemblies of Actors are automatically supplied.