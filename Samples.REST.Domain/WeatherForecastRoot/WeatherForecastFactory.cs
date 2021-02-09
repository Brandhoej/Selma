using System;

namespace Samples.REST.Domain.WeatherForecastRoot
{
    public interface IWeatherForecastFactory
    {
        IWeatherForecast CreateWeatherForecast(DateTime dateTime, string summary, int celcius);
    }

    internal class WeatherForecastFactory
        : IWeatherForecastFactory
    {
        public IWeatherForecast CreateWeatherForecast(DateTime dateTime, string summary, int celcius)
            => new WeatherForecast(dateTime, summary, celcius);
    }
}
