using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class CityDal : BaseCityDal
{
    public CityDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}