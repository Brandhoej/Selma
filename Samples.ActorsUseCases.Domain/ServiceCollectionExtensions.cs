using Microsoft.Extensions.DependencyInjection;
using Samples.ActorsUseCases.Domain.ProfileRoot;

namespace Samples.ActorsUseCases.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProfileFactory>(provider => new ProfileFactory());

            serviceCollection.AddScoped(provider => new DomainContext());
            serviceCollection.AddScoped(provider => new UnitOfWork(provider.GetService<DomainContext>()));
            serviceCollection.AddScoped(provider => provider.GetService<UnitOfWork>().ProfileRepository);
            return serviceCollection;
        }
    }
}
