using Microsoft.Extensions.Configuration;

namespace AppCore.WebUI;

public class AppSettingsUtil : BaseAppSettingsUtil
{
    public AppSettingsUtil(IConfiguration configuration) 
        : base(configuration)
    {
    }
}