using AppCore.Business.Configs;

namespace AppCore.Business.Results;

public class SuccessResult : Result
{
    public SuccessResult() 
        : base(ResultStatusConfig.Success, "")
    {
        
    }
    
    public SuccessResult(string message) 
        : base(ResultStatusConfig.Success, message)
    {
    }
}