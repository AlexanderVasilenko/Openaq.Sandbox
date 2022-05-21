using System.Collections.Generic;
using System.Threading.Tasks;
using Openaq.Sandbox.MVP.Domain.Services.Openaq;

namespace Openaq.Sandbox.MVP.Core.Infrastructure
{
    public interface IOpenaqService
    {
        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns></returns>
        Task<List<Country>> GetCountries();

        /// <summary>
        /// Get all cities by country
        /// </summary>
        /// <param name="countryCode">Country code</param>
        /// <returns></returns>
        Task<List<CityObject>> GetCitiesByCountry(string countryCode);

        /// <summary>
        /// Get measurement by city
        /// </summary>
        /// <param name="city">City</param>
        /// <returns></returns>
        Task<List<LocationObject>> GetMeasurementByCity(string city);
    }
}
