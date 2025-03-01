using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FileManager.Domain.Configuration;
using Microsoft.Extensions.Options;

namespace FileManager.Infrastructure.Authentication;

public class JwtHelper
{
    public static string GenerateJwtToken(string userLogin, IOptions<JwtOptions> options)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim> { new(ClaimTypes.Name, userLogin) };

        var token = new JwtSecurityToken(
            options.Value.Issuer,
            options.Value.Audience,
            claims,
            expires: DateTime.UtcNow.AddHours(options.Value.ExpirationInHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}