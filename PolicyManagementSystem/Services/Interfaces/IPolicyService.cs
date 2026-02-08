using PolicyManagementSystem.DTOs.Policy;

namespace PolicyManagementSystem.Services.Interfaces
{
    public interface IPolicyService
    {
        Task CreatePolicyAsync(CreatePolicyRequestDto dto);
        Task UpdatePolicyAsync(int id, UpdatePolicyRequestDto dto);
        Task UpdatePolicyStatusAsync(int id, UpdatePolicyStatusDto dto);
        Task<List<PolicyResponseDto>> GetAllAsync();
        Task<List<PolicyResponseDto>> GetActiveAsync();
        Task<PolicyResponseDto> GetByIdAsync(int id);
        Task EnrollUserAsync(int userId, int policyId);
    }
}
