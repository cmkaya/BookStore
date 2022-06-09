using AppCore.Business.Configs;
using Business.Abstracts;
using Business.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class CountriesController : Controller
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public IActionResult Index()
    {
        return View(_countryService.Query().ToList());
    }

    // GET: Countries/Edit/1
    public IActionResult Edit(int? id)
    {
        if (!id.HasValue)
            return View("NotFound");

        var countryDataResult = _countryService.GetCountryById(id.Value);
        if (countryDataResult.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(countryDataResult.Message);

        if (countryDataResult.ResultStatus == ResultStatusConfig.Error)
            return View("NotFound");

        return View(countryDataResult.Data);
    }

    // POST: Countries/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CountryDto dto)
    {
        if (ModelState.IsValid)
        {
            var countryResult = _countryService.Update(dto);
            switch (countryResult.ResultStatus)
            {
                case ResultStatusConfig.Exception:
                    throw new Exception(countryResult.Message);
                case ResultStatusConfig.Success:
                    return RedirectToAction(nameof(Index));
                default:
                    ModelState.AddModelError("", countryResult.Message);
                    break;
            }
        }

        return View(dto);
    }
    
    // GET: Countries/Create
    public IActionResult Create()
    {
        return View(new CountryDto());
    }

    // POST: Countries/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CountryDto dto)
    {
        if (ModelState.IsValid)
        {
            var countryResult = _countryService.Add(dto);
            switch (countryResult.ResultStatus)
            {
                case ResultStatusConfig.Exception:
                    throw new Exception(countryResult.Message);
                case ResultStatusConfig.Success:
                    return RedirectToAction(nameof(Index));
                default:
                    ModelState.AddModelError("", countryResult.Message);
                    break;
            }
        }

        return View(dto);
    }

    public IActionResult Delete(int? id)
    {
        if (!id.HasValue)
            return View("NotFound");

        var countryResult = _countryService.Delete(id.Value);
        if (countryResult.ResultStatus == ResultStatusConfig.Error)
            return View("Error");

        return RedirectToAction(nameof(Index));
    }
}