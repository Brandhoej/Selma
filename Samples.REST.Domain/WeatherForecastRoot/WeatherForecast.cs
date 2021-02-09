using Selma.Core.Domain;
using Selma.Core.Domain.Abstractions;
using System;

namespace Samples.REST.Domain.WeatherForecastRoot
{
    public interface IWeatherForecast
        : IEntityRoot
    {
        DateTime Date { get; }
        string Summary { get; }
        int TemperatureC { get; }
        int TemperatureF { get; }
    }

    internal class WeatherForecast
        : EntityRoot
        , IWeatherForecast
    {
        private WeatherForecast()
        { }

        internal WeatherForecast(DateTime date, string summary, int temperatureC)
        {
            Date = date;
            Summary = summary;
            TemperatureC = temperatureC;
        }

        public DateTime Date { get; set; }
        public string Summary { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}