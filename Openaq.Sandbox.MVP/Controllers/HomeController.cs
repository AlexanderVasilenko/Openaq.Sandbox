using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Openaq.Sandbox.MVP.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Openaq.Sandbox.MVP.Core.Infrastructure;

namespace Openaq.Sandbox.MVP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOpenaqService _openaqService;

        public HomeController(ILogger<HomeController> logger, IOpenaqService openaqService)
        {
            _logger = logger;
            _openaqService = openaqService;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _openaqService.GetCountries();
            ViewBag.Countries = countries?.Select(c => new SelectListItem { Text = c.Name, Value = c.Code }).ToList();
            return View(countries);
        }

        public async Task<IActionResult> ShowCities(string country)
        {
            var cities = await _openaqService.GetCitiesByCountry(country);
            if (cities == null || !cities.Any())
                return PartialView("_NoResult");

            ViewBag.Cities = cities?.Select(c => new SelectListItem { Text = c.City, Value = c.City }).ToList();
            return PartialView("_CitiesSelectPartial", cities);
        }

        public async Task<IActionResult> ShowMeasurementByCity(string city)
        {
            var measurementsLocations = await _openaqService.GetMeasurementByCity(city);
            if(measurementsLocations == null || !measurementsLocations.Any())
                return PartialView("_NoResult");

            foreach (var location in measurementsLocations)
            {
                location.Measurements.OrderBy(x => x.Parameter);
            }

            return PartialView("_MeasurementTablePartial", measurementsLocations);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
