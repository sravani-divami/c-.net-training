using Microsoft.EntityFrameworkCore;
using MyFirstWebApiProj.Data;
using MyFirstWebApiProj.Models.Entities;

namespace MyFirstWebApiProj.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentEntity>> GetAllStudents()
        {
            return await _context.students_college.ToListAsync();
        }
    }
}
