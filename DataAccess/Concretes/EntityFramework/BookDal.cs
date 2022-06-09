using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class BookDal : BaseBookDal
{
    public BookDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}