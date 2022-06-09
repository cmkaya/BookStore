using AppCore.Business.Configs;

namespace AppCore.Business.Results;

public class Result
{
    public Result(ResultStatusConfig resultStatus, string message)
    {
        ResultStatus = resultStatus;
        Message = message;
    }

    public string Message { get; }
    public ResultStatusConfig ResultStatus { get; }
}