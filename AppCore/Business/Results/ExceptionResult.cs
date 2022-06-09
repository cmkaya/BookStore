using AppCore.Business.Configs;

namespace AppCore.Business.Results;

public class ExceptionResult : Result
{
    public ExceptionResult()
        : base(ResultStatusConfig.Exception, "")
    {
    }

    public ExceptionResult(Exception exception, bool showExceptionMessage = true)
        : base(ResultStatusConfig.Exception,
            showExceptionMessage
                ? (exception != null
                    ? "Exception: " + exception.Message + (exception.InnerException != null
                        ? " | Inner Exception: " + exception.InnerException.Message +
                          (exception.InnerException.InnerException != null
                              ? " | :" + exception.InnerException.InnerException.Message
                              : "")
                        : "")
                    : "")
                : "") 

    {
    }
}