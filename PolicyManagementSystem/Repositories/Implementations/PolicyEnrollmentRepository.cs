using Microsoft.EntityFrameworkCore;
using PolicyManagementSystem.Data;
using PolicyManagementSystem.Entities;
using PolicyManagementSystem.Repositories.Interfaces;

namespace PolicyManagementSystem.Repositories.Implementations
{
    public class PolicyEnrollmentRepository : IPolicyEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public PolicyEnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PolicyEnrollment enrollment)
        {
            _context.PolicyEnrollments.Add(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task<PolicyEnrollment?> GetByIdAsync(int id)
        {
            return await _context.PolicyEnrollments
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<PolicyEnrollment>> GetByUserIdAsync(int userId)
        {
            return await _context.PolicyEnrollments
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<PolicyEnrollment>> GetByStatusAsync(string status)
        {
            return await _context.PolicyEnrollments
                .Where(e => e.Status == status)
                .ToListAsync();
        }

        public async Task<List<PolicyEnrollment>> GetAllAsync()
        {
            return await _context.PolicyEnrollments.ToListAsync();
        }

        public async Task UpdateAsync(PolicyEnrollment enrollment)
        {
            _context.PolicyEnrollments.Update(enrollment);
            await _context.SaveChangesAsync();
        }
    }
}
