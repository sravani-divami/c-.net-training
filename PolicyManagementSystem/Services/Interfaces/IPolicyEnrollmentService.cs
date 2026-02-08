using PolicyManagementSystem.DTOs.PolicyEnrollment;

namespace PolicyManagementSystem.Services.Interfaces
{
    public interface IPolicyEnrollmentService
    {
        Task RequestEnrollmentAsync(int userId, RequestPolicyEnrollmentDto dto);
        Task<List<PolicyEnrollmentResponseDto>> GetMyEnrollmentsAsync(int userId);
        Task<List<PolicyEnrollmentResponseDto>> GetPendingEnrollmentsAsync();
        Task<List<PolicyEnrollmentResponseDto>> GetAllEnrollmentsAsync();
        Task UpdateEnrollmentStatusAsync(int enrollmentId, UpdateEnrollmentStatusDto dto);
        Task ApproveEnrollmentAsync(int enrollmentId);
        Task RejectEnrollmentAsync(int enrollmentId);
    }
}
