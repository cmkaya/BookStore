using AppCore.Business.Configs;
using Business.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Route("[controller]")]
public class CitiesJsonController : Controller
{
    private readonly ICityService _cityService;

    public CitiesJsonController(ICityService cityService)
    {
        _cityService = cityService;
    }
    
    // GET: Cities/CitiesGet
    [Route("CitiesGet/{countryId:int?}")] // ~/CitiesJson/GetCities/1
    public IActionResult GetCities(int? countryId)
    {
        if (!countryId.HasValue)
            return View("NotFound");
    
        var citiesDataResult = _cityService.GetCitiesByCountryId(countryId.Value);
        if (citiesDataResult.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(citiesDataResult.Message);
    
        return Json(citiesDataResult.Data);
    }
    
    // POST: Cities/CitiesPost
    [HttpPost]
    [Route("CitiesPost/{countryId:int?}")] // ~/CitiesJson/PostCities/1
    public IActionResult PostCities(int? countryId)
    {
        if (!countryId.HasValue)
            return View("NotFound");
    
        var citiesDataResult = _cityService.GetCitiesByCountryId(countryId.Value);
        if (citiesDataResult.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(citiesDataResult.Message);
    
        return Json(citiesDataResult.Data);
    }
}