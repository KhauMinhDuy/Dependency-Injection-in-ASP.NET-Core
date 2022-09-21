using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisBookings.Web.External.Models;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using Microsoft.Extensions.Logging;

namespace TennisBookings.Web.External
{
    public class WeatherApiClient : IWeatherApiClient
    {
        public HttpClient _httpClient { get; }
        public ILogger<WeatherApiClient> _logger { get; }

        public WeatherApiClient(HttpClient httpClient, IOptions<ExternalServicesConfig> options, ILogger<WeatherApiClient> logger)
        {
            var externalServicesConfig = options.Value;
            httpClient.BaseAddress = new Uri(externalServicesConfig.WeatherApiUrl);

            this._logger = logger;
            this._httpClient = httpClient;

        }

        public async Task<WeatherApiResult> GetWeatherForecastAsync()
        {
            const string path = "api/currentWeather/Brighton";
            try
            {
                var response = await _httpClient.GetAsync(path);
                if(!response.IsSuccessStatusCode) 
                {
                    return null;
                }

                var content = await response.Content.ReadAsAsync<WeatherApiResult>();
                return content;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Failed to get weather data from API");
            }

            return null;
        }
    }
}