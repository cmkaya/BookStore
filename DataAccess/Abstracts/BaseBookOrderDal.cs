using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseBookOrderDal : EfBaseRepositoryCore<BookOrder>
{
    protected BaseBookOrderDal(DbContext dbContext) : base(dbContext)
    {
    }
}