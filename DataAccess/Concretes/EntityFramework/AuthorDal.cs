using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class AuthorDal : BaseAuthorDal
{
    public AuthorDal(DbContext dbContext) 
        : base(dbContext)
    {
    }
}