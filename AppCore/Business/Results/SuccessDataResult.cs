using AppCore.Business.Configs;

namespace AppCore.Business.Results;

public class SuccessDataResult<TData> : DataResult<TData>
{
    public SuccessDataResult() 
        : base(ResultStatusConfig.Success, "", default)
    {
    }
    
    public SuccessDataResult(string message) 
        : base(ResultStatusConfig.Success, message, default)
    {
    }
    
    public SuccessDataResult(TData data) 
        : base(ResultStatusConfig.Success, "", data)
    {
    }
    
    public SuccessDataResult(string message, TData data) 
        : base(ResultStatusConfig.Success, message, data)
    {
    }
}