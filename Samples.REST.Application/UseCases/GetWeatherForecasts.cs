using Samples.REST.Domain.WeatherForecastRoot;
using Selma.Core.Application;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.REST.Application.UseCases
{
    public class GetWeatherForecastsRequest
        : UseCaseRequest<GetWeatherForecastsResponse>
    { }

    public class GetWeatherForecastsResponse
    {
        public GetWeatherForecastsResponse(IEnumerable<IWeatherForecast> weatherForecasts)
            => WeatherForecasts = weatherForecasts;

        public IEnumerable<IWeatherForecast> WeatherForecasts { get; }
    }

    internal class GetWeatherForecasts
        : UseCase<GetWeatherForecastsRequest, GetWeatherForecastsResponse>
    {
        public GetWeatherForecasts(IWeatherForecastRepository weatherForecastRepository)
            => WeatherForecastRepository = weatherForecastRepository;

        private IWeatherForecastRepository WeatherForecastRepository { get; }

        public override Task<GetWeatherForecastsResponse> Handle(GetWeatherForecastsRequest request, CancellationToken cancellationToken = default)
            => Task.FromResult(new GetWeatherForecastsResponse(WeatherForecastRepository.GetAll()));
    }
}
