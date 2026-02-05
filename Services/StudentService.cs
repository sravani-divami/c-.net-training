using MyFirstWebApiProj.Models.Entities;
using MyFirstWebApiProj.Repositories;

namespace MyFirstWebApiProj.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StudentEntity>> GetAllStudents()
        {
            return await _repository.GetAllStudents();
        }
    }
}
