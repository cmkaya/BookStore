namespace AppCore.Business.Results;

public interface IDataResult<out TData>
{
    TData Data { get; }
}