using System.Security.Cryptography;
using System.Text;

namespace FileManager.Infrastructure.Authentication;

public class CryptoHelper
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
}