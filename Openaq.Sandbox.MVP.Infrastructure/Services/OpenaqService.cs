using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Openaq.Sandbox.MVP.Core.Exceptions;
using Openaq.Sandbox.MVP.Core.Infrastructure;
using Openaq.Sandbox.MVP.Domain.Services.Openaq;

namespace Openaq.Sandbox.MVP.Infrastructure.Services
{
    public class OpenaqService:IOpenaqService
    {
        private readonly ServiceSettings _serviceSettings;
        private readonly ILogger<ServiceSettings> _logger;
        static HttpClient _client;
        public OpenaqService(IOptions<ServiceSettings> serviceSettings, ILogger<ServiceSettings> logger)
        {
            _serviceSettings = serviceSettings.Value;
            _logger = logger;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_serviceSettings.ServicePath)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Country>> GetCountries()
        {
            List<Country> countries = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync("v2/countries?limit=200&page=1&offset=0&sort=asc&order_by=country");
            
                if (response.IsSuccessStatusCode)
                {
                    var customersString = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<JObject>(customersString);
                    countries = jsonResponse.Root.Last.Last.ToObject<List<Country>>();
                }
            }
            catch(ApiException ex)
            {
                _logger.LogError($"v2/countries call exception {ex.Message}");
            }

            return countries;
        }

        public async Task<List<CityObject>> GetCitiesByCountry(string countryCode)
        {
            List<CityObject> cities = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(
                    $"v2/cities?limit=100000&page=1&offset=0&sort=asc&country_id={countryCode}&order_by=city");
                if (response.IsSuccessStatusCode)
                {
                    var customersString = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<JObject>(customersString);
                    cities = jsonResponse.Root.Last.Last.ToObject<List<CityObject>>();
                }
            }
            catch (ApiException ex)
            {
                _logger.LogError($"v2/cities call exception {ex.Message}");
            }

            return cities;
        }

        public async Task<List<LocationObject>> GetMeasurementByCity(string city)
        {
            List<LocationObject> measurement = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(
                    $"v2/latest?limit=1000&page=1&offset=0&sort=desc&radius=50&city={city}&order_by=lastUpdated&dumpRaw=false");
                if (response.IsSuccessStatusCode)
                {
                    var customersString = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<JObject>(customersString);
                    measurement = jsonResponse.Root.Last.Last.ToObject<List<LocationObject>>();
                }
            }
            catch (ApiException ex)
            {
                _logger.LogError($"v2/latest call exception {ex.Message}");
            }

            return measurement;
        }
    }
}
