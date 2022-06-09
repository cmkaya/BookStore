using AppCore.Business.Configs;

namespace AppCore.Business.Results;

public class ErrorDataResult<TData> : DataResult<TData>
{
    public ErrorDataResult()
        : base(ResultStatusConfig.Error, "", default)
    {
    }
    
    public ErrorDataResult(string message)
        : base(ResultStatusConfig.Error, message, default)
    {
    }
    
    public ErrorDataResult(TData data)
        : base(ResultStatusConfig.Error, "", data)
    {
    }
    
    public ErrorDataResult(string message, TData data) 
        : base(ResultStatusConfig.Error, message, data)
    {
    }
}