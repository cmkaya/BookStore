using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseGenreDal : EfBaseRepositoryCore<Genre>
{
    protected BaseGenreDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}