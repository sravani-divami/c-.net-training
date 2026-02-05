using Microsoft.AspNetCore.Mvc;
using MyFirstWebApiProj.Services;
using MyFirstWebApiProj.Models.Entities;

namespace MyFirstWebApiProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<List<StudentEntity>>> GetAllStudents()
        {
            var students = await _service.GetAllStudents();
            return Ok(students);
        }
    }
}
