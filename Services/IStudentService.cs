using MyFirstWebApiProj.Models.Entities;

namespace MyFirstWebApiProj.Services
{
    public interface IStudentService
    {
        Task<List<StudentEntity>> GetAllStudents();
    }
}
