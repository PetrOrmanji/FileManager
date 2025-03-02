using Microsoft.AspNetCore.Http;
using EnsureThat;

namespace FileManager.Application.Services;

public class UserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        EnsureArg.IsNotNull(httpContextAccessor, nameof(httpContextAccessor));

        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("UserId");

        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }

        throw new UnauthorizedAccessException("User ID is not found in claims.");
    }
}