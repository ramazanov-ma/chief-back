using Chief.Application.DTOs;
using Chief.Domain.Entities;

namespace Chief.Application.Interfaces;

public interface ITelegramAuthService
{
    Task<User> AuthenticateUserAsync(TelegramAuthDto telegramAuth);
    Task<bool> ValidateTelegramHashAsync(TelegramAuthDto telegramAuth);
    Task<string> GenerateApiTokenAsync(User user);

    bool ValidateInitData(string initData);
    Task<User> AuthenticateWebAppUserAsync(TelegramUserDto telegramUser)
}