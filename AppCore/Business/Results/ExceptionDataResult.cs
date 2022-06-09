using AppCore.Business.Configs;

namespace AppCore.Business.Results;

public class ExceptionDataResult<TData> : DataResult<TData>
{
    public ExceptionDataResult()
        : base(ResultStatusConfig.Exception, "", default)
    {
    }

    public ExceptionDataResult(Exception exception, bool showExceptionMessage = true)
        : base(ResultStatusConfig.Exception,
            showExceptionMessage
                ? exception != null
                    ? "Exception: " + exception.Message + (exception.InnerException != null
                        ? " | Inner Exception: " + exception.InnerException.Message +
                          (exception.InnerException.InnerException != null
                              ? " | " + exception.InnerException.InnerException.Message
                              : "")
                        : "")
                    : ""
                : "", default)
    {
    }
}