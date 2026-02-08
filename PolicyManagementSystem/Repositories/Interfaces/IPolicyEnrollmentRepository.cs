using PolicyManagementSystem.Entities;

namespace PolicyManagementSystem.Repositories.Interfaces
{
    public interface IPolicyEnrollmentRepository
    {
        Task AddAsync(PolicyEnrollment enrollment);
        Task<PolicyEnrollment?> GetByIdAsync(int id);
        Task<List<PolicyEnrollment>> GetByUserIdAsync(int userId);
        Task<List<PolicyEnrollment>> GetByStatusAsync(string status);
        Task<List<PolicyEnrollment>> GetAllAsync();
        Task UpdateAsync(PolicyEnrollment enrollment);
    }
}
