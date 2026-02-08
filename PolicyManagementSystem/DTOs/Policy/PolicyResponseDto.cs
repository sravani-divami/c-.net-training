namespace PolicyManagementSystem.DTOs.Policy
{
    public class PolicyResponseDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyName { get; set; }
        public string Description { get; set; }
        public int PremiumAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
