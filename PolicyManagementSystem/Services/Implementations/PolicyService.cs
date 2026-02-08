using PolicyManagementSystem.DTOs.Policy;
using PolicyManagementSystem.Entities;
using PolicyManagementSystem.Exceptions;
using PolicyManagementSystem.Repositories.Interfaces;
using PolicyManagementSystem.Services.Interfaces;

namespace PolicyManagementSystem.Services.Implementations
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _repo;
        private readonly IPolicyEnrollmentRepository _enrollmentRepo;

        public PolicyService(IPolicyRepository repo, IPolicyEnrollmentRepository enrollmentRepo)
        {
            _repo = repo;
            _enrollmentRepo = enrollmentRepo;
        }

        public async Task CreatePolicyAsync(CreatePolicyRequestDto dto)
        {
            var policy = new Policy
            {
                PolicyNumber = dto.PolicyNumber,
                PolicyName = dto.PolicyName,
                Description = dto.Description,
                PremiumAmount = dto.PremiumAmount,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(policy);
        }

        public async Task UpdatePolicyAsync(int id, UpdatePolicyRequestDto dto)
        {
            var policy = await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException("Policy");

            policy.PolicyNumber = dto.PolicyNumber;
            policy.PolicyName = dto.PolicyName;
            policy.Description = dto.Description;
            policy.PremiumAmount = dto.PremiumAmount;
            policy.IsActive = dto.IsActive;
            policy.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(policy);
        }

        public async Task UpdatePolicyStatusAsync(int id, UpdatePolicyStatusDto dto)
        {
            var policy = await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException("Policy");

            policy.IsActive = dto.IsActive;
            policy.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(policy);
        }

        public async Task<List<PolicyResponseDto>> GetAllAsync()
            => (await _repo.GetAllAsync()).Select(Map).ToList();

        public async Task<List<PolicyResponseDto>> GetActiveAsync()
            => (await _repo.GetActiveAsync()).Select(Map).ToList();

        public async Task<PolicyResponseDto> GetByIdAsync(int id)
            => Map(await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException("Policy"));

        public async Task EnrollUserAsync(int userId, int policyId)
        {
            var policy = await _repo.GetByIdAsync(policyId);
            if (policy == null)
                throw new NotFoundException("Policy");

            if (!policy.IsActive)
                throw new BadRequestException("Policy is not active");

            // Check if user already enrolled in this policy
            var existingEnrollments = await _enrollmentRepo.GetByUserIdAsync(userId);
            if (existingEnrollments.Any(e => e.PolicyId == policyId))
            {
                throw new DuplicateResourceException("User is already enrolled in this policy");
            }

            var enrollment = new PolicyEnrollment
            {
                UserId = userId,
                PolicyId = policyId,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _enrollmentRepo.AddAsync(enrollment);
        }

        private static PolicyResponseDto Map(Policy p) => new()
        {
            Id = p.Id,
            PolicyNumber = p.PolicyNumber,
            PolicyName = p.PolicyName,
            Description = p.Description,
            PremiumAmount = p.PremiumAmount,
            IsActive = p.IsActive
        };
    }
}
