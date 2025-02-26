using Chief.Application.DTOs;
using Chief.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chief.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OnboardingController(IOnboardingService onboardingService) : ControllerBase
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
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
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private int GetCurrentUserId()
        {
            // Получаем ID пользователя из клеймов токена
            // Это зависит от вашей реализации аутентификации
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst("sub");

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user identity");
            }

            return userId;
        }
    }
}