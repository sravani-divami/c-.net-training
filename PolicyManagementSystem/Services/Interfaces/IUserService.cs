using PolicyManagementSystem.DTOs.User;

namespace PolicyManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task<List<UserEnrollmentResponseDto>> GetUserEnrollmentsAsync(int userId);
    }
}
