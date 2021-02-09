using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Samples.REST.Application;
using Samples.REST.Application.UseCases;
using System.Threading.Tasks;

namespace Samples.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
            => _logger = logger;

        public ILogger<WeatherForecastController> Logger { get; }

        [HttpGet]
        public async Task<GetWeatherForecastsResponse> Get([FromServices] IUser user)
            => (await user.Do(new GetWeatherForecastsRequest()));
    }
}
