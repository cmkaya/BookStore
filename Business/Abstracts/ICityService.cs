using AppCore.Business.Results;
using AppCore.Business.Services;
using Business.DataTransferObjects;

namespace Business.Abstracts;

public interface ICityService : IServiceCore<CityDto>
{
    DataResult<List<CityDto>> GetCitiesByCountryId(int? countryId = null);
    DataResult<CityDto> GetCityById(int id);
}