using MyFirstWebApiProj.Models.Entities;

namespace MyFirstWebApiProj.Repositories
{
    public interface IStudentRepository
    {
        Task<List<StudentEntity>> GetAllStudents();
    }
}