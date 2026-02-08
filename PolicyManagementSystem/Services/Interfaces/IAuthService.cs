using PolicyManagementSystem.DTOs.Auth;

namespace PolicyManagementSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto registerDto);

        Task<string> LoginAsync(LoginRequestDto loginDto);
    }
}
