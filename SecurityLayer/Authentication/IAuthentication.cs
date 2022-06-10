using System.IdentityModel.Tokens.Jwt;

namespace E_Commerce.Api.SecurityLayer.Authentication;

public interface IAuthentication
{
    string GenerateJwt(string userName, IEnumerable<string> roles);
}