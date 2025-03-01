using EnsureThat;
using FileManager.Application.Services;
using FileManager.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FileManagerAuthService _fileManagerAuthService;

        public AuthController(FileManagerAuthService fileManagerAuthService)
        {
            EnsureArg.IsNotNull(fileManagerAuthService, nameof(fileManagerAuthService));

            _fileManagerAuthService = fileManagerAuthService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _fileManagerAuthService.LoginAsync(request.Login, request.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            if (await _fileManagerAuthService.RegisterAsync(request.Login, request.Password))
                return Ok(new { message = "Registration successful" });

            return BadRequest(new { message = "User already exists" });
        }
    }
}
