using AppCore.Records;

namespace AppCore.DataAccess;

public interface IRepositoryCore<TEntity> : IDisposable where TEntity : BaseRecordCore, new()
{
    IQueryable<TEntity> Query(params string[] entitiesToInclude);
    
    /// <summary>
    /// Inserts a record to the database.
    /// </summary>
    /// <param name="entity">Represents an entity.</param>
    /// <param name="acceptAllChanges">Indicates whether to call <see cref="Save"/> to post successfully to the database.</param>
    void Add(TEntity entity, bool acceptAllChanges = true);
    
    /// <summary>
    /// Updates a record in the database.
    /// </summary>
    /// <param name="entity">Represents an entity.</param>
    /// <param name="acceptAllChanges">Indicates whether to call <see cref="Save"/> to post successfully to the database.</param>
    void Update(TEntity entity, bool acceptAllChanges = true);
    
    /// <summary>
    /// Updates a record in the database.
    /// </summary>
    /// <param name="entity">Represents an entity.</param>
    /// <param name="acceptAllChanges">Indicates whether to call <see cref="Save"/> to post successfully to the database.</param>
    void Delete(TEntity entity, bool acceptAllChanges = true);
    
    /// <summary>
    /// Save all changes.
    /// </summary>
    /// <returns></returns>
    int Save();
}