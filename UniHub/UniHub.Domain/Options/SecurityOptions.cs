namespace UniHub.Domain.Options;

public class SecurityOptions
{
    public string? ApiKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? SecretKey { get; set; }
    public string? ExpirationTime { get; set; }
}
