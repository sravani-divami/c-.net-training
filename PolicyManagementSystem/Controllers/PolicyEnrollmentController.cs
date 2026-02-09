using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyManagementSystem.DTOs.PolicyEnrollment;
using PolicyManagementSystem.Services.Interfaces;
using System.Security.Claims;

namespace PolicyManagementSystem.Controllers
{
    [ApiController]
    [Route("api")]
    public class PolicyEnrollmentController : ControllerBase
    {
        private readonly IPolicyEnrollmentService _service;

        public PolicyEnrollmentController(IPolicyEnrollmentService service)
        {
            _service = service;
        }

        // USER APIs
        // View My Enrollments
        // GET: api/my/enrollments
        [Authorize]
        [HttpGet("my/enrollments")]
        public async Task<IActionResult> MyEnrollments()
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            return Ok(await _service.GetMyEnrollmentsAsync(userId));
        }

        // ADMIN APIs
        // View Pending Enrollments (with optional status filter)
        // GET: api/admin/enrollments?status=Pending
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/enrollments")]
        public async Task<IActionResult> GetEnrollments([FromQuery] string? status = null)
        {
            if (!string.IsNullOrEmpty(status) && 
                status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            {
                return Ok(await _service.GetPendingEnrollmentsAsync());
            }
            return Ok(await _service.GetAllEnrollmentsAsync());
        }

        // Approve Enrollment
        // POST: api/admin/enrollments/{id}/approve
        [Authorize(Roles = "Admin")]
        [HttpPost("admin/enrollments/{id}/approve")]
        public async Task<IActionResult> ApproveEnrollment(int id)
        {
            await _service.ApproveEnrollmentAsync(id);
            return Ok("Enrollment approved successfully");
        }

        // Reject Enrollment
        // POST: api/admin/enrollments/{id}/reject
        [Authorize(Roles = "Admin")]
        [HttpPost("admin/enrollments/{id}/reject")]
        public async Task<IActionResult> RejectEnrollment(int id)
        {
            await _service.RejectEnrollmentAsync(id);
            return Ok("Enrollment rejected successfully");
        }
    }
}
