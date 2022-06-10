using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Commerce.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.Api.SecurityLayer.Authentication;

public class Authentication : IAuthentication
{
    private readonly IJwtConfiguration _configuration;

    public Authentication(IJwtConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwt(string userName, IEnumerable<string> roles)
    {
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SecretKey));
        var token = new JwtSecurityToken(  
            issuer: _configuration.ValidIssuer,
            claims: authClaims,  
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
        ); 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}