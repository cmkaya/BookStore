using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseAuthorDal : EfBaseRepositoryCore<Author>
{
    protected BaseAuthorDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}