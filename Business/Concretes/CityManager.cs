using AppCore.Business.Results;
using Business.Abstracts;
using Business.Constants;
using Business.DataTransferObjects;
using DataAccess.Abstracts;
using Entities;

namespace Business.Concretes;

public class CityManager : ICityService
{
    private readonly BaseCityDal _cityDal;

    public CityManager(BaseCityDal cityDal)
    {
        _cityDal = cityDal;
    }

    public IQueryable<CityDto> Query()
    {
        return _cityDal.Query("Country").Select(c => new CityDto
        {
            Id = c.Id,
            CountryId = c.CountryId,
            Guid = c.Guid,
            Name = c.Name,
            Country = new CountryDto
            {
                Id = c.Country.Id,
                Guid = c.Country.Guid,
                Name = c.Country.Name
            }
        });
    }

    //Return the data result of city list based on country id.
    public DataResult<List<CityDto>> GetCitiesByCountryId(int? countryId = null)
    {
        try
        {
            var query = Query();
            if (countryId.HasValue)
                query = query.Where(c => c.CountryId == countryId);

            return new SuccessDataResult<List<CityDto>>(query.ToList());
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<List<CityDto>>(exc);
        }
    }
    
    //Return the result of city data based on city id.
    public DataResult<CityDto> GetCityById(int id)
    {
        try
        {
            var cityDataResult = Query().SingleOrDefault(c => c.Id == id);
            if (cityDataResult is null)
                return new ErrorDataResult<CityDto>(Messages.NoCityFound);

            return new SuccessDataResult<CityDto>(cityDataResult);
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<CityDto>(exc);
        }
    }

    public Result Add(CityDto dto)
    {
        try
        {
            if (_cityDal.Query().Any(c => c.Name.ToLower() == dto.Name.ToLower().Trim()))
                return new ErrorResult(Messages.ExistedCity);

            var cityToCreate = new City
            {
                CountryId = dto.CountryId,
                Guid = dto.Guid,
                Name = dto.Name.Trim()
            };
            
            _cityDal.Add(cityToCreate);
            return new SuccessResult(Messages.CityAdded);

        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public Result Update(CityDto dto)
    {
        try
        {
            if (_cityDal.Query().Any(c => c.Name.ToLower() == dto.Name.ToLower().Trim() && c.Id != dto.Id))
                return new ErrorResult(Messages.ExistedCity);

            var cityToUpdate = _cityDal.Query("Country").SingleOrDefault(c => c.Id == dto.Id);
            if (cityToUpdate is not null)
            {
                cityToUpdate.CountryId = dto.CountryId;
                cityToUpdate.Guid = dto.Guid;
                cityToUpdate.Name = dto.Name.Trim();
            }

            _cityDal.Update(cityToUpdate);
            return new SuccessResult(Messages.CityUpdated);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public Result Delete(int id)
    {
        try
        {
            var cityToDelete = _cityDal.Query("UserDetails").SingleOrDefault(c => c.Id == id);
            if (cityToDelete is null)
                return new ErrorResult(Messages.NoCityFound);

            if (cityToDelete.UserDetails.Count > 0)
                return new ErrorResult(Messages.InvalidCityRemove);
            
            _cityDal.Delete(cityToDelete);
            return new SuccessResult(Messages.CityDeleted);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }
    
    public void Dispose()
    {
        _cityDal.Dispose();
    }
}