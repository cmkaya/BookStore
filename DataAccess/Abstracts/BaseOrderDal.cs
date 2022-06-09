using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseOrderDal : EfBaseRepositoryCore<Order>
{
    protected BaseOrderDal(DbContext dbContext) : base(dbContext)
    {
    }
}