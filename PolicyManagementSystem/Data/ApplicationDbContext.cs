using Microsoft.EntityFrameworkCore;
using PolicyManagementSystem.Entities;

namespace PolicyManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyEnrollment> PolicyEnrollments { get; set; }

    }
}
