using AppCore.Business.Results;
using AppCore.Records;

namespace AppCore.Business.Services;

/// <summary>
/// Includes the fundamental methods for request processing.
/// </summary>
/// <typeparam name="TDto">Represents the modified entities which are called models, in other words data transfer objects.</typeparam>
public interface IServiceCore<TDto> : IDisposable where TDto : BaseRecordCore, new()
{
    IQueryable<TDto> Query();
    Result Add(TDto dto);
    Result Update(TDto dto);
    Result Delete(int id);
}