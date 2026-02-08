using PolicyManagementSystem.Entities;

namespace PolicyManagementSystem.Repositories.Interfaces
{
    public interface IPolicyRepository
    {
        Task<List<Policy>> GetAllAsync();
        Task<List<Policy>> GetActiveAsync();
        Task<Policy?> GetByIdAsync(int id);
        Task AddAsync(Policy policy);
        Task UpdateAsync(Policy policy);
    }
}
