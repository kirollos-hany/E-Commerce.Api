namespace E_Commerce.Api.Models;

public interface IJwtConfiguration
{
    public string SecretKey { get; set; } 

    public string ValidIssuer { get; set; }
}