using Microsoft.AspNetCore.Http;

namespace FileManager.Infrastructure.Extensions;

public static class FormFileExtensions
{
    public static async Task<byte[]> ToBytesAsync(this IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}