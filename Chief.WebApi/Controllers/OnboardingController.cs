using System.Security.Claims;
using Chief.Application.DTOs;
using Chief.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chief.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OnboardingController(
        IOnboardingService onboardingService,
        ILogger<OnboardingController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = GetCurrentUserId();
                var profile = await onboardingService.GetProfileByUserIdAsync(userId);
                return Ok(profile);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogWarning(ex, "Profile not found");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving onboarding profile");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateOnboardingProfileDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var updatedProfile = await onboardingService.UpdateProfileAsync(id, dto, userId);
                return Ok(updatedProfile);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogWarning(ex, "Profile not found for update");
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogWarning(ex, "Unauthorized profile update attempt");
                return Forbid();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating onboarding profile");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user identity");
            }

            return userId;
        }
    }
}