using Microsoft.Extensions.Configuration;

namespace AppCore.WebUI;

public class BaseAppSettingsUtil
{
    private readonly IConfiguration _configuration;

    public BaseAppSettingsUtil(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual T Bind<T>(string sectionKey = "AppSettings") where T : class, new()
    {
        T t = null;
        IConfiguration section = _configuration.GetSection(sectionKey);
        if (section is not null)
        {
            t = new T();
            section.Bind(t);
        }

        return t;
    }
}