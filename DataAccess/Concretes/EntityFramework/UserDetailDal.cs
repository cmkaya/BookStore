using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class UserDetailDal : BaseUserDetailDal
{
    public UserDetailDal(DbContext dbContext) : base(dbContext)
    {
    }
}