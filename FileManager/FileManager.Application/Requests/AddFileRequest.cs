using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Application.Requests;

public record AddFileRequest(
    [FromForm] IFormFile File,
    [FromForm] Guid? FolderId);