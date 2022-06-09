using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class OrderDal : BaseOrderDal
{
    public OrderDal(DbContext dbContext) : base(dbContext)
    {
    }
}