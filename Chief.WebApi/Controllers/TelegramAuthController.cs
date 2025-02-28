using Chief.Application.DTOs;
using Chief.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chief.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelegramAuthController(
    ITelegramAuthService telegramAuthService,
    ILogger<TelegramAuthController> logger)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] TelegramAuthDto telegramAuth)
    {
        try
        {
            var user = await telegramAuthService.AuthenticateUserAsync(telegramAuth);
            var token = await telegramAuthService.GenerateApiTokenAsync(user);

            return Ok(new 
            { 
                token, 
                userId = user.Id,
                telegramUserId = user.TelegramUserId,
                firstName = user.FirstName,
                lastName = user.LastName
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning(ex, "Failed Telegram authentication attempt");
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during Telegram authentication");
            return StatusCode(500, "Internal server error during authentication");
        }
    }
}