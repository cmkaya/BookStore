using System.Security.Claims;
using AppCore.Business.Configs;
using AppCore.Business.Results;
using Business.Abstracts;
using Business.DataTransferObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Controllers;

public class AccountsController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;

    public AccountsController(IAccountService accountService, ICountryService countryService, ICityService cityService)
    {
        _accountService = accountService;
        _countryService = countryService;
        _cityService = cityService;
    }
    
    public IActionResult Login()
    {
        return View(new UserLoginDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        if (ModelState.IsValid)
        {
            DataResult<UserDto> dataResultUser = _accountService.Login(dto);
            if (dataResultUser.ResultStatus == ResultStatusConfig.Exception)
            {
                // throw new Exception(dataResultUser.Message);
                return View("Error");
            }

            if (dataResultUser.ResultStatus == ResultStatusConfig.Success)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dataResultUser.Data.UserName),
                    new Claim(ClaimTypes.Role, dataResultUser.Data.Role.Name),
                    new Claim(ClaimTypes.Sid, dataResultUser.Data.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.AddModelError("", dataResultUser.Message);
        }

        return View(dto);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    public IActionResult Register()
    {
        DataResult<List<CountryDto>> dataResultCountries = _countryService.GetAllCountries();
        if (dataResultCountries.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultCountries.Message);

        ViewBag.Countries = new SelectList(dataResultCountries.Data, "Id", "Name");
        return View(new UserRegisterDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(UserRegisterDto dto)
    {
        if (ModelState.IsValid)
        {
            var resultUser = _accountService.Register(dto);
            switch (resultUser.ResultStatus)
            {
                case ResultStatusConfig.Exception:
                    throw new Exception(resultUser.Message);
                case ResultStatusConfig.Success:
                    return Redirect(nameof(Login));
                default:
                    ModelState.AddModelError("", resultUser.Message);
                    break;
            }
        }

        DataResult<List<CountryDto>> dataResultCountries = _countryService.GetAllCountries();
        DataResult<List<CityDto>> dataResultCities = _cityService.GetCitiesByCountryId(dto.UserDetail.CountryId);
        if (dataResultCountries.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultCountries.Message);

        if (dataResultCities.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultCities.Message);

        ViewBag.Countries = new SelectList(dataResultCountries.Data, "Id", "Name", dto.UserDetail.CountryId);
        ViewBag.Cities = new SelectList(dataResultCities.Data, "Id", "Name", dto.UserDetail.CityId);
        return View(dto);
    }
}