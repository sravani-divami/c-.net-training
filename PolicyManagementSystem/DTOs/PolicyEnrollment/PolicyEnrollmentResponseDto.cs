namespace PolicyManagementSystem.DTOs.PolicyEnrollment
{
    public class PolicyEnrollmentResponseDto
    {
        public int EnrollmentId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyNumber { get; set; }
        public string Status { get; set; }
        public DateTime? RequestedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
