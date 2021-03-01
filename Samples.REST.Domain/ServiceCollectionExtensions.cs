using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Samples.REST.Domain.WeatherForecastRoot;
using Selma.Core.MessageQueue.MediatR;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;
using Selma.Core.Domain.Events.Abstractions;

namespace Samples.REST.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
        {
            serviceCollection.AddDbContext<DomainContext>(dbContextOptionsBuilder);
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<DomainContext>>();
            serviceCollection.AddImmediateMessageQueue<IDomainEvent>();

            serviceCollection.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            serviceCollection.AddScoped<IWeatherForecastFactory, WeatherForecastFactory>();
            return serviceCollection;
        }
    }
}
