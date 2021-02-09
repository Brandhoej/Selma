using Microsoft.Extensions.DependencyInjection;
using Selma.Core.Application;

namespace Samples.REST.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddActors(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddActor<IUser, User>();
            return serviceCollection;
        }
    }
}
