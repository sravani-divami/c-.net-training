using Microsoft.AspNetCore.Mvc;
using PolicyManagementSystem.DTOs.Auth;
using PolicyManagementSystem.Services.Interfaces;

namespace PolicyManagementSystem.Controllers
{
    [ApiController] // class to handle api requests.
    [Route("api/auth")]
    public class AuthController : ControllerBase // ControllerBase is a base class for an MVC controller without view support. It provides properties and methods for handling HTTP requests and generating responses.
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerDto)
        {
            await _authService.RegisterAsync(registerDto);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { token });
        }
    }
}
