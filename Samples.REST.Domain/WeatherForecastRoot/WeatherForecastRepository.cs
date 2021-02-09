using Selma.Core.Infrastructure.Persistent.Abstractions;
using System.Collections.Generic;

namespace Samples.REST.Domain.WeatherForecastRoot
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<IWeatherForecast> GetAll();
    }

    internal sealed class WeatherForecastRepository
        : IWeatherForecastRepository
    {
        public WeatherForecastRepository(IUnitOfWork unitOfWork)
            : this(unitOfWork.Repository<WeatherForecast>())
        { }

        public WeatherForecastRepository(IRepository<WeatherForecast> repository)
            => Repository = repository;

        private IRepository<WeatherForecast> Repository { get; }

        public IEnumerable<IWeatherForecast> GetAll()
            => Repository.ReadAll();
    }
}
