namespace E_Commerce.Api.Models;

public class JwtConfiguration : IJwtConfiguration
{
    public string SecretKey { get; set; } = string.Empty;

    public string ValidIssuer { get; set; } = string.Empty;
}