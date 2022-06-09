using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class CountryDal : BaseCountryDal
{
    public CountryDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}