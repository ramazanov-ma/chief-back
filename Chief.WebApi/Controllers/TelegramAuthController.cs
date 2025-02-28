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
    [HttpPost("initData")]
    public async Task<IActionResult> ProcessInitData([FromBody] TelegramWebAppDto initDataDto)
    {
        try
        {
            // Проверка подписи initData
            bool isValid = telegramAuthService.ValidateInitData(initDataDto.InitData);
            if (!isValid)
            {
                return Unauthorized("Invalid Telegram data signature");
            }

            // Создание или обновление пользователя
            var user = await telegramAuthService.AuthenticateWebAppUserAsync(initDataDto.User);

            // Генерация JWT токена
            var token = await telegramAuthService.GenerateApiTokenAsync(user);

            return Ok(new
            {
                token,
                user = new
                {
                    id = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    telegramId = user.TelegramUserId
                }
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing Telegram WebApp initData");
            return StatusCode(500, "Error authenticating with Telegram");
        }
    }
}