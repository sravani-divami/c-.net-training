using Microsoft.EntityFrameworkCore;
using MyFirstWebApiProj.Models.Entities;

namespace MyFirstWebApiProj.Data
{       
    // DbContext: database instance
        public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }

        // DbSet: a query object, represents a table in the database, not data in the memory or not a collection or not a data structure
        public DbSet<StudentEntity> students_college { get; set; }
    }
}
