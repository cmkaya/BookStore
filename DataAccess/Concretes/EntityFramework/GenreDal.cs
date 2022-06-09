using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class GenreDal : BaseGenreDal
{
    public GenreDal(DbContext dbContext) : base(dbContext)
    {
    }
}