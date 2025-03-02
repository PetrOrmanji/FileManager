using FileManager.Infrastructure.Authentication;
using Xunit;

namespace FileManager.Tests.TestClasses;

public class CryptoTests
{
    [Fact]
    public void HashPassword_ValidPassword_ReturnsExpectedHash()
    {
        var password = "TestPassword123";
        var expectedHash = "1Rk5ek6Jp6ZtKKJm7QCmeb3uk/3eyeu6fQH/J8OcGpk=";

        var result = CryptoHelper.HashPassword(password);

        Assert.Equal(expectedHash, result);
    }

    [Theory]
    [InlineData("password1")]
    [InlineData("anotherPassword")]
    [InlineData("someRandomPassword")]
    public void HashPassword_SameInput_ReturnsSameHash(string password)
    {
        var result1 = CryptoHelper.HashPassword(password);
        var result2 = CryptoHelper.HashPassword(password);

        Assert.Equal(result1, result2);
    }

    [Theory]
    [InlineData("password1", "password2")]
    [InlineData("test123", "test1234")]
    public void HashPassword_DifferentPasswords_ReturnsDifferentHashes(string password1, string password2)
    {
        var result1 = CryptoHelper.HashPassword(password1);
        var result2 = CryptoHelper.HashPassword(password2);

        Assert.NotEqual(result1, result2);
    }
}