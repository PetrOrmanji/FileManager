using FileManager.Domain.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using FileManager.Infrastructure.Authentication;
using Xunit;

namespace FileManager.Tests.TestClasses;

public class JwtTests
{
    [Fact]
    public void GenerateJwtToken_ValidInput_ReturnsValidJwtToken()
    {
        var userLogin = "testUser";
        var userId = "123";
        var options = Options.Create(new JwtOptions
        {
            SecretKey = "superSecretKey123superSecretKey123superSecretKey123",
            Issuer = "myIssuer",
            Audience = "myAudience",
            ExpirationInHours = 1
        });

        var token = JwtHelper.GenerateJwtToken(userLogin, userId, options);

        Assert.NotNull(token); 

        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);

        Assert.Equal(options.Value.Issuer, jwtToken.Issuer);
        Assert.Equal(options.Value.Audience, jwtToken.Audiences.FirstOrDefault());

        var userLoginClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserLogin");
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");

        Assert.NotNull(userLoginClaim);
        Assert.Equal(userLogin, userLoginClaim.Value);

        Assert.NotNull(userIdClaim);
        Assert.Equal(userId, userIdClaim.Value);

        var expiration = jwtToken.ValidTo;
        Assert.True(expiration > DateTime.UtcNow);
        Assert.True(expiration < DateTime.UtcNow.AddHours(options.Value.ExpirationInHours));
    }

    [Fact]
    public void GenerateJwtToken_InvalidSecretKey_ThrowsException()
    {
        var userLogin = "testUser";
        var userId = "123";
        var options = Options.Create(new JwtOptions
        {
            SecretKey = "",
            Issuer = "myIssuer",
            Audience = "myAudience",
            ExpirationInHours = 1
        });

        Assert.Throws<ArgumentException>(() => JwtHelper.GenerateJwtToken(userLogin, userId, options));
    }
}