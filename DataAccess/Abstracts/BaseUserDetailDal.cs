using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseUserDetailDal : EfBaseRepositoryCore<UserDetail>
{
    protected BaseUserDetailDal(DbContext dbContext) : base(dbContext)
    {
    }
}