using System.ComponentModel.DataAnnotations;

namespace PolicyManagementSystem.DTOs.Policy
{
    public class CreatePolicyRequestDto
    {
        [Required(ErrorMessage = "Policy number is required")]
        public required string PolicyNumber { get; set; }

        [Required(ErrorMessage = "Policy name is required")]
        public required string PolicyName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Premium amount is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Premium amount must be greater than 0")]
        public int PremiumAmount { get; set; }

        public bool IsActive { get; set; }
    }
}
