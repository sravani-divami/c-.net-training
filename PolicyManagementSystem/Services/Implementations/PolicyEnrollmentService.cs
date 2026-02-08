using PolicyManagementSystem.DTOs.PolicyEnrollment;
using PolicyManagementSystem.Entities;
using PolicyManagementSystem.Exceptions;
using PolicyManagementSystem.Repositories.Interfaces;
using PolicyManagementSystem.Services.Interfaces;

namespace PolicyManagementSystem.Services.Implementations
{
    public class PolicyEnrollmentService : IPolicyEnrollmentService
    {
        private readonly IPolicyEnrollmentRepository _enrollmentRepo;
        private readonly IPolicyRepository _policyRepo;

        public PolicyEnrollmentService(
            IPolicyEnrollmentRepository enrollmentRepo,
            IPolicyRepository policyRepo)
        {
            _enrollmentRepo = enrollmentRepo;
            _policyRepo = policyRepo;
        }

        public async Task RequestEnrollmentAsync(int userId, RequestPolicyEnrollmentDto dto)
        {
            var policy = await _policyRepo.GetByIdAsync(dto.PolicyId);
            if (policy == null)
                throw new NotFoundException("Policy");

            if (!policy.IsActive)
                throw new BadRequestException("Policy is not active");

            var enrollment = new PolicyEnrollment
            {
                UserId = userId,
                PolicyId = dto.PolicyId,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _enrollmentRepo.AddAsync(enrollment);
        }

        public async Task<List<PolicyEnrollmentResponseDto>> GetMyEnrollmentsAsync(int userId)
        {
            var enrollments = await _enrollmentRepo.GetByUserIdAsync(userId);
            var policies = await _policyRepo.GetAllAsync();

            return enrollments.Select(e =>
            {
                var p = policies.First(pol => pol.Id == e.PolicyId);
                return new PolicyEnrollmentResponseDto
                {
                    EnrollmentId = e.Id,
                    PolicyName = p.PolicyName,
                    PolicyNumber = p.PolicyNumber,
                    Status = e.Status,
                    RequestedAt = e.RequestedAt,
                    ApprovedAt = e.ApprovedAt
                };
            }).ToList();
        }

        public async Task<List<PolicyEnrollmentResponseDto>> GetPendingEnrollmentsAsync()
        {
            var enrollments = await _enrollmentRepo.GetByStatusAsync("Pending");
            var policies = await _policyRepo.GetAllAsync();

            return enrollments.Select(e =>
            {
                var p = policies.First(pol => pol.Id == e.PolicyId);
                return new PolicyEnrollmentResponseDto
                {
                    EnrollmentId = e.Id,
                    PolicyName = p.PolicyName,
                    PolicyNumber = p.PolicyNumber,
                    Status = e.Status,
                    RequestedAt = e.RequestedAt,
                    ApprovedAt = e.ApprovedAt
                };
            }).ToList();
        }

        public async Task UpdateEnrollmentStatusAsync(
            int enrollmentId,
            UpdateEnrollmentStatusDto dto)
        {
            var enrollment = await _enrollmentRepo.GetByIdAsync(enrollmentId)
                ?? throw new NotFoundException("Enrollment");

            enrollment.Status = dto.Status;
            enrollment.ApprovedAt = DateTime.UtcNow;
            enrollment.UpdatedAt = DateTime.UtcNow;

            await _enrollmentRepo.UpdateAsync(enrollment);
        }

        public async Task<List<PolicyEnrollmentResponseDto>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _enrollmentRepo.GetAllAsync();
            var policies = await _policyRepo.GetAllAsync();

            return enrollments.Select(e =>
            {
                var p = policies.First(pol => pol.Id == e.PolicyId);
                return new PolicyEnrollmentResponseDto
                {
                    EnrollmentId = e.Id,
                    PolicyName = p.PolicyName,
                    PolicyNumber = p.PolicyNumber,
                    Status = e.Status,
                    RequestedAt = e.RequestedAt,
                    ApprovedAt = e.ApprovedAt
                };
            }).ToList();
        }

        public async Task ApproveEnrollmentAsync(int enrollmentId)
        {
            var enrollment = await _enrollmentRepo.GetByIdAsync(enrollmentId)
                ?? throw new NotFoundException("Enrollment");

            enrollment.Status = "Approved";
            enrollment.ApprovedAt = DateTime.UtcNow;
            enrollment.UpdatedAt = DateTime.UtcNow;

            await _enrollmentRepo.UpdateAsync(enrollment);
        }

        public async Task RejectEnrollmentAsync(int enrollmentId)
        {
            var enrollment = await _enrollmentRepo.GetByIdAsync(enrollmentId)
                ?? throw new NotFoundException("Enrollment");

            enrollment.Status = "Rejected";
            enrollment.ApprovedAt = DateTime.UtcNow;
            enrollment.UpdatedAt = DateTime.UtcNow;

            await _enrollmentRepo.UpdateAsync(enrollment);
        }
    }
}
