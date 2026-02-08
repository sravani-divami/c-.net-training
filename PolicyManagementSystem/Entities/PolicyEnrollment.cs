using System.ComponentModel.DataAnnotations.Schema;

namespace PolicyManagementSystem.Entities
{
    [Table("policy_enrollment")]
    public class PolicyEnrollment
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("policy_id")]
        public int PolicyId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("status")]
        public string Status { get; set; }   // Pending / Approved / Rejected

        [Column("requested_at")]
        public DateTime? RequestedAt { get; set; }

        [Column("approved_at")]
        public DateTime? ApprovedAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
