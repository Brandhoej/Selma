using Microsoft.EntityFrameworkCore;
using Samples.REST.Domain.WeatherForecastRoot;
using Selma.Core.Infrastructure.Persistent.EntityFramework;

namespace Samples.REST.Domain
{
    public class DomainContext
        : Context
    {
        public DomainContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        { }

        private DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
