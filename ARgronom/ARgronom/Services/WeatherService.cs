using ARgronom.Models.Weather;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ARgronom.Services
{
    public class WeatherService
    {
        private readonly HttpClient _client;

        private readonly string _apiKey;

        public WeatherService(IConfiguration config)
        {
            _client = new HttpClient();
            _apiKey = config.GetValue<string>("WeatherApiKey");
        }

        public async Task<WeatherApiResponse> GetWeather(decimal lat, decimal lon)
        {
            string requestUrl = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
            var response = await _client.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<WeatherApiResponse>(response);
        }
    }
}
