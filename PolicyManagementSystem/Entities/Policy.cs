using System.ComponentModel.DataAnnotations.Schema;

namespace PolicyManagementSystem.Entities
{
    [Table("policy")]
    public class Policy
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("policy_number")]
        public string PolicyNumber { get; set; }

        [Column("policy_name")]
        public string PolicyName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("premium_amount")]
        public int PremiumAmount { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
