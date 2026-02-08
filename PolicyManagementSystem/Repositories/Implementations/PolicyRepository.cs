using Microsoft.EntityFrameworkCore;
using PolicyManagementSystem.Data;
using PolicyManagementSystem.Entities;
using PolicyManagementSystem.Repositories.Interfaces;

namespace PolicyManagementSystem.Repositories.Implementations
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ApplicationDbContext _context;

        public PolicyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Policy>> GetAllAsync()
            => await _context.Policies.ToListAsync();

        public async Task<List<Policy>> GetActiveAsync()
            => await _context.Policies.Where(p => p.IsActive).ToListAsync();

        public async Task<Policy?> GetByIdAsync(int id)
            => await _context.Policies.FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Policy policy)
        {
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Policy policy)
        {
            _context.Policies.Update(policy);
            await _context.SaveChangesAsync();
        }
    }
}
