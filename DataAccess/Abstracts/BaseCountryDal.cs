using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseCountryDal : EfBaseRepositoryCore<Country>
{
    protected BaseCountryDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}