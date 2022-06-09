using System.Linq.Expressions;
using AppCore.Records;
using Microsoft.EntityFrameworkCore;

namespace AppCore.DataAccess.EntityFramework;

public abstract class EfBaseRepositoryCore<TEntity> : IRepositoryCore<TEntity> where TEntity 
    : BaseRecordCore, new()
{
    private readonly DbContext _dbContext;

    protected EfBaseRepositoryCore(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Query(params string[] entitiesToInclude)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();
        foreach (var entity in entitiesToInclude)
            query = query.Include(entity);
        return query;
    }

    public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate,
        params string[] entitiesToInclude)
    {
        var query = Query(entitiesToInclude);
        return query.Where(predicate);
    }

    public void Add(TEntity entity, bool acceptAllChanges = true)
    {
        try
        {
            entity.Guid = Guid.NewGuid().ToString();
            _dbContext.Set<TEntity>().Add(entity);
            if (acceptAllChanges)
                Save();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void Update(TEntity entity, bool acceptAllChanges = true)
    {
        try
        {
            _dbContext.Set<TEntity>().Update(entity);
            if (acceptAllChanges)
                Save();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void Delete(TEntity entity, bool acceptAllChanges = true)
    {
        try
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (acceptAllChanges)
                Save();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public virtual void Delete(int id)
    {
        try
        {
            TEntity entity = Query(e => e.Id == id).SingleOrDefault();
            Delete(entity);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public int Save()
    {
        try
        {
            return _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            throw e;
        }
            
    }
    
    public void Dispose()
    {
        _dbContext?.Dispose();
        GC.SuppressFinalize(this);
    }
}