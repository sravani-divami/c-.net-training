using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyManagementSystem.Services.Interfaces;

namespace PolicyManagementSystem.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // ADMIN: get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _service.GetAllUsersAsync());
        }

        // ADMIN: get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _service.GetUserByIdAsync(id));
        }

        // ADMIN: get user enrollments
        [HttpGet("{id}/enrollments")]
        public async Task<IActionResult> GetUserEnrollments(int id)
        {
            return Ok(await _service.GetUserEnrollmentsAsync(id));
        }
    }
}
