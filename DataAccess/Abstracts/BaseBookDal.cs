using AppCore.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstracts;

public abstract class BaseBookDal : EfBaseRepositoryCore<Book>
{
    protected BaseBookDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}