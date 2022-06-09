using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseUserDal : EfBaseRepositoryCore<User>
{
    protected BaseUserDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}