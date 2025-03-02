using EnsureThat;
using FileManager.Domain.Configuration;
using FileManager.Domain.Entities;
using FileManager.Infrastructure.Authentication;
using FileManager.Infrastructure.Persistence.Repositories.Entities;
using Microsoft.Extensions.Options;

namespace FileManager.Application.Services;

public class FileManagerAuthService
{
    private readonly FileManagerUserRepository _fileManagerUserRepository;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public FileManagerAuthService(FileManagerUserRepository fileManagerUserRepository, IOptions<JwtOptions> jwtOptions)
    {
        EnsureArg.IsNotNull(fileManagerUserRepository, nameof(fileManagerUserRepository));

        _fileManagerUserRepository = fileManagerUserRepository;
        _jwtOptions = jwtOptions;
    }

    public async Task<string?> LoginAsync(string login, string password)
    {
        var user = await _fileManagerUserRepository.GetAsync(login);
        if (user == null || user.PasswordHash != CryptoHelper.HashPassword(password))
            return null;

        return JwtHelper.GenerateJwtToken(login, user.Id.ToString(), _jwtOptions);
    }

    public async Task<bool> RegisterAsync(string login, string password)
    {
        var user = await _fileManagerUserRepository.GetAsync(login);
        if (user != null) return false;

        var passwordHash = CryptoHelper.HashPassword(password);
        var newUser = new FileManagerUser
        {
            Login = login,
            PasswordHash = passwordHash
        };

        await _fileManagerUserRepository.AddAsync(newUser);
        await _fileManagerUserRepository.SaveChangesAsync();
        return true;
    }
}