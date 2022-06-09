using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseCityDal : EfBaseRepositoryCore<City>
{
    protected BaseCityDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}