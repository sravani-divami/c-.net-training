using PolicyManagementSystem.DTOs.User;
using PolicyManagementSystem.Repositories.Interfaces;
using PolicyManagementSystem.Services.Interfaces;

namespace PolicyManagementSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPolicyEnrollmentRepository _enrollmentRepo;
        private readonly IPolicyRepository _policyRepo;

        public UserService(
            IUserRepository userRepo,
            IPolicyEnrollmentRepository enrollmentRepo,
            IPolicyRepository policyRepo)
        {
            _userRepo = userRepo;
            _enrollmentRepo = enrollmentRepo;
            _policyRepo = policyRepo;
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            return (await _userRepo.GetAllAsync())
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role
                }).ToList();
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id)
                ?? throw new Exception("User not found");

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<List<UserEnrollmentResponseDto>> GetUserEnrollmentsAsync(int userId)
        {
            var enrollments = await _enrollmentRepo.GetByUserIdAsync(userId);
            var policies = await _policyRepo.GetAllAsync();

            return enrollments.Select(e =>
            {
                var p = policies.First(pol => pol.Id == e.PolicyId);

                return new UserEnrollmentResponseDto
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
    }
}
