using AppCore.Business.Results;
using Business.Abstracts;
using Business.Constants;
using Business.DataTransferObjects;
using DataAccess.Abstracts;
using Entities;

namespace Business.Concretes;

public class CountryManager : ICountryService
{
    private readonly BaseCountryDal _countryDal;

    public CountryManager(BaseCountryDal countryDal)
    {
        _countryDal = countryDal;
    }

    public IQueryable<CountryDto> Query()
    {
        return _countryDal.Query().Select(co => new CountryDto
        {
            Id = co.Id,
            Guid = co.Guid,
            Name = co.Name
        });
    }

    public DataResult<List<CountryDto>> GetAllCountries()
    {
        try
        {
            var countries = Query().ToList();
            return new SuccessDataResult<List<CountryDto>>(countries);
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<List<CountryDto>>(exc);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //Retrieve the result with country data based on country id.
    public DataResult<CountryDto> GetCountryById(int id)
    {
        try
        {
            var countryDto = Query().SingleOrDefault(c => c.Id == id);
            if (countryDto is null)
                return new ErrorDataResult<CountryDto>(Messages.NoCountryFound);

            return new SuccessDataResult<CountryDto>(countryDto);
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<CountryDto>(exc);
        }
    }

    public Result Add(CountryDto dto)
    {
        try
        {
            if (_countryDal.Query().Any(co => co.Name.ToUpper() == dto.Name.ToUpper().Trim()))
                return new ErrorResult(Messages.ExistedCountry);

            var countryToAdd = new Country
            {
                Guid = dto.Guid,
                Name = dto.Name.Trim()
            };
            
            _countryDal.Add(countryToAdd);
            return new SuccessResult(Messages.CountryAdded);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public Result Update(CountryDto dto)
    {
        try
        {
            if (_countryDal.Query().Any(co => co.Name.ToUpper() == dto.Name.ToUpper().Trim() && co.Id != dto.Id))
                return new ErrorResult(Messages.ExistedCountry);

            var countryToUpdate = _countryDal.Query().SingleOrDefault(co => co.Id == dto.Id);
            if (countryToUpdate != null)
            {
                countryToUpdate.Guid = dto.Guid;
                countryToUpdate.Name = dto.Name.Trim();
            }
            
            _countryDal.Update(countryToUpdate);
            return new SuccessResult(Messages.CountryUpdated);
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
            var countryToDelete = _countryDal.Query("Cities", "UserDetails").SingleOrDefault(co => co.Id == id);
            if (countryToDelete is null)
                return new ErrorResult(Messages.NoCountryFound);
            
            if (countryToDelete.Cities.Count > 0)
                return new ErrorResult(Messages.InvalidCountryRemove);
            
            if (countryToDelete.UserDetails.Count > 0)
                return new ErrorResult(Messages.InvalidCountryRemove2);
            
            _countryDal.Delete(countryToDelete);
            return new SuccessResult(Messages.CountryDeleted);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }
    
    public void Dispose()
    {
        _countryDal.Dispose();
    }
}