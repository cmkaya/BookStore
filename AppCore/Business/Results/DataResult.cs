using AppCore.Business.Configs;

namespace AppCore.Business.Results;

public class DataResult<TData> : Result, IDataResult<TData>
{
    public DataResult(ResultStatusConfig resultStatus, string message, TData data) 
        : base(resultStatus, message)
    {
        Data = data;
    }

    public TData Data { get; }
}