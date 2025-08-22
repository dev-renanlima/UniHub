using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UniHub.Infrastructure.Authentication;

namespace UniHub.CrossCutting.OptionsSetup;

public class SecurityOptionSetup : IConfigureOptions<SecurityOptions>
{
    private const string SectionName = "Security"; 
    private readonly IConfiguration _configuration;

    public SecurityOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(SecurityOptions options) 
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
