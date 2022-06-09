using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class UserDal : BaseUserDal
{
    public UserDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}