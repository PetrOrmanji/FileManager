using EnsureThat;
using FileManager.Application.Requests;
using FileManager.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class FileManagerController : ControllerBase
{
    private readonly FileManagerService _fileManagerService;
    private readonly UserContextService _userContextService;

    public FileManagerController(FileManagerService fileManagerService, UserContextService userContextService)
    {
        EnsureArg.IsNotNull(fileManagerService, nameof(fileManagerService));
        EnsureArg.IsNotNull(userContextService, nameof(userContextService));

        _fileManagerService = fileManagerService;
        _userContextService = userContextService;
    }

    [HttpGet("getFolder")]
    public async Task<IActionResult> GetFolder([FromQuery] Guid? folderId)
    {
        var userId = _userContextService.GetUserId();

        var folderDto = await _fileManagerService.GetFolder(folderId, userId);

        return folderDto is null ? NotFound() : Ok(folderDto);
    }

    [HttpPost("addFolder")]
    public async Task<IActionResult> AddFolder([FromBody] AddFolderRequest request)
    {
        var userId = _userContextService.GetUserId();

        await _fileManagerService.AddFolder(request, userId);

        return Ok();
    }

    [HttpDelete("deleteFolder")]
    public async Task<IActionResult> DeleteFolder([FromBody] DeleteFolderRequest request)
    {
        var userId = _userContextService.GetUserId();

        await _fileManagerService.DeleteFolder(request, userId);

        return Ok();
    }

    [HttpGet("getFile")]
    public async Task<IActionResult> GetFile([FromQuery] Guid fileId, Guid? folderId)
    {
        var userId = _userContextService.GetUserId();

        var fileDto = await _fileManagerService.GetFile(fileId, folderId, userId);

        return fileDto is null ? NotFound() : Ok(fileDto);
    }

    [HttpPost("addFile")]
    public async Task<IActionResult> AddFile([FromForm] AddFileRequest request)
    {
        var userId = _userContextService.GetUserId();

        await _fileManagerService.AddFile(request, userId);

        return Ok();
    }

    [HttpDelete("deleteFile")]
    public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequest request)
    {
        var userId = _userContextService.GetUserId();

        await _fileManagerService.DeleteFile(request, userId);

        return Ok();
    }
}