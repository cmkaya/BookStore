using AppCore.Business.Results;
using AppCore.Business.Services;
using Business.DataTransferObjects;

namespace Business.Abstracts;

public interface ICountryService : IServiceCore<CountryDto>
{
    DataResult<List<CountryDto>> GetAllCountries();
    DataResult<CountryDto> GetCountryById(int id);
}