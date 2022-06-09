using AppCore.Business.Configs;
using AppCore.Business.Results;
using Business.Abstracts;
using Business.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Controllers;

public class CitiesController : Controller
{
    private readonly ICityService _cityService;
    private readonly ICountryService _countryService;

    public CitiesController(ICityService cityService, ICountryService countryService)
    {
        _cityService = cityService;
        _countryService = countryService;
    }
    
    // GET: Cities/Index/1
    public IActionResult Index()
    {
        return View(_cityService.Query().ToList());
    }
    
    // GET: Cities/Detail/2
    public IActionResult Edit(int? id)
    {
        if (id is null)
            return View("NotFound");

        DataResult<CityDto> dataResultCities = _cityService.GetCityById(id.Value);
        if (dataResultCities.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultCities.Message);
        
        if (dataResultCities.ResultStatus == ResultStatusConfig.Error)
            return View("NotFound");

        var dataResultCountries = _countryService.GetAllCountries();
        if (dataResultCountries.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultCountries.Message);

        ViewBag.Countries = new SelectList(dataResultCountries.Data, "Id", "Name", dataResultCities.Data.CountryId);
        return View(dataResultCities.Data);
    }
    
    // POST: Cities/Edit/2
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CityDto dto)
    {
        if (ModelState.IsValid)
        {
            var cityResult = _cityService.Update(dto);
            switch (cityResult.ResultStatus)
            {
                case ResultStatusConfig.Exception:
                    throw new Exception(cityResult.Message);
                case ResultStatusConfig.Success:
                    return RedirectToAction(nameof(Index));
                default:
                    ModelState.AddModelError("", cityResult.Message);
                    break;
            }
        }

        var dataResultCountries = _countryService.GetAllCountries();
        if (dataResultCountries.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultCountries.Message);

        ViewBag.Countries = new SelectList(dataResultCountries.Data, "Id", "Name", dto.CountryId);
        return View(dto);
    }
    
    // GET: Cities/Create/3
    public IActionResult Create()
    {
        var dataResultCountries = _countryService.GetAllCountries();
        if (dataResultCountries.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultCountries.Message);

        ViewBag.Countries = new SelectList(dataResultCountries.Data, "Id", "Name");
        return View(new CityDto());
    }

    // POST: Cities/Create/3
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CityDto dto)
    {
        if (ModelState.IsValid)
        {
            var cityResult = _cityService.Add(dto);
            switch (cityResult.ResultStatus)
            {
                case ResultStatusConfig.Exception:
                    throw new Exception(cityResult.Message);
                case ResultStatusConfig.Success:
                    return RedirectToAction(nameof(Index));
                default:
                    ModelState.AddModelError("", cityResult.Message);
                    break;
            }
        }

        var countriesDataResult = _countryService.GetAllCountries();
        if (countriesDataResult.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(countriesDataResult.Message);

        ViewBag.Countries = new SelectList(countriesDataResult.Data, "Id", "Name", dto.CountryId);
        return View(dto);
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    public IActionResult Delete(int? id)
    {
        if (!id.HasValue)
            return View("NotFound");

        var result = _cityService.Delete(id.Value);
        if (result.ResultStatus == ResultStatusConfig.Error)
            return View("Error");

        return RedirectToAction(nameof(Index));
    }
}