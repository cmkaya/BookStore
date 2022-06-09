using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class BookOrderDal : BaseBookOrderDal
{
    public BookOrderDal(DbContext dbContext) : base(dbContext)
    {
    }
}