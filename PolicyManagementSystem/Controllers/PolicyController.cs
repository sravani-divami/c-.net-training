using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyManagementSystem.DTOs.Policy;
using PolicyManagementSystem.Services.Interfaces;

namespace PolicyManagementSystem.Controllers
{
    [ApiController]
    [Route("api")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        // USER APIs
        // Get Available Policies (only active)
        // GET: api/policies
        [Authorize]
        [HttpGet("policies")]
        public async Task<IActionResult> GetAvailablePolicies()
        {
            var policies = await _policyService.GetActiveAsync();
            return Ok(policies);
        }

        // Request Policy Enrollment
        // POST: api/policies/{policyId}/enroll
        [Authorize]
        [HttpPost("policies/{policyId}/enroll")]
        public async Task<IActionResult> EnrollInPolicy(int policyId)
        {
            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            
            if (userRole == "Admin")
            {
                throw new Exceptions.ForbiddenException("Admins cannot enroll in policies");
            }

            await _policyService.EnrollUserAsync(userId, policyId);
            return Ok("Enrollment requested successfully");
        }

        // ADMIN APIs
        // Add Policy
        // POST: api/admin/policies
        [Authorize(Roles = "Admin")]
        [HttpPost("admin/policies")]
        public async Task<IActionResult> CreatePolicy(
            [FromBody] CreatePolicyRequestDto dto)
        {
            await _policyService.CreatePolicyAsync(dto);
            return Ok("Policy created successfully");
        }

        // Update Policy
        // PUT: api/admin/policies/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("admin/policies/{id}")]
        public async Task<IActionResult> UpdatePolicy(
            int id,
            [FromBody] UpdatePolicyRequestDto dto)
        {
            await _policyService.UpdatePolicyAsync(id, dto);
            return Ok("Policy updated successfully");
        }

        // Activate / Deactivate Policy
        // PATCH: api/admin/policies/{id}/status
        [Authorize(Roles = "Admin")]
        [HttpPatch("admin/policies/{id}/status")]
        public async Task<IActionResult> UpdatePolicyStatus(
            int id,
            [FromBody] UpdatePolicyStatusDto dto)
        {
            await _policyService.UpdatePolicyStatusAsync(id, dto);
            return Ok("Policy status updated successfully");
        }
    }
}
