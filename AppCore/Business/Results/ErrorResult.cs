using AppCore.Business.Configs;

namespace AppCore.Business.Results;

public class ErrorResult : Result
{
    public ErrorResult() 
        : base(ResultStatusConfig.Error, "")
    {
        
    }
    
    public ErrorResult(string message) 
        : base(ResultStatusConfig.Error, message)
    {
    }
}