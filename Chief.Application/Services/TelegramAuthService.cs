using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Chief.Application.DTOs;
using Chief.Application.Interfaces;
using Chief.Domain.Entities;
using Chief.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Chief.Application.Services;

public sealed class TelegramAuthService(
    IUserRepository userRepository,
    IConfiguration configuration) : ITelegramAuthService
{
    private readonly string? _botToken = configuration["Telegram:BotToken"];
    private readonly string? _jwtSecret = configuration["JWT:Secret"];
    private readonly string? _jwtIssuer = configuration["JWT:Issuer"];
    private readonly string? _jwtAudience = configuration["JWT:Audience"];

    public async Task<User> AuthenticateUserAsync(TelegramAuthDto telegramAuth)
    {
        // Проверяем валидность хэша от Telegram
        bool isValid = await ValidateTelegramHashAsync(telegramAuth);
        if (!isValid)
        {
            throw new UnauthorizedAccessException("Invalid Telegram authentication data");
        }

        // Ищем пользователя по Telegram ID
        var user = await userRepository.GetByTelegramIdAsync(telegramAuth.UserId);

        // Если пользователь не существует, создаем нового
        if (user == null)
        {
            user = new User
            {
                TelegramUserId = telegramAuth.UserId,
                FirstName = telegramAuth.FirstName,
                LastName = telegramAuth.LastName,
                TelegramUsername = telegramAuth.Username,
                LanguageCode = telegramAuth.LanguageCode,
                CreatedAt = DateTime.UtcNow
            };

            await userRepository.CreateAsync(user);
        }
        else
        {
            // Обновляем данные пользователя, если они изменились
            var needsUpdate = false;

            if (user.FirstName != telegramAuth.FirstName)
            {
                user.FirstName = telegramAuth.FirstName;
                needsUpdate = true;
            }

            if (user.LastName != telegramAuth.LastName)
            {
                user.LastName = telegramAuth.LastName;
                needsUpdate = true;
            }

            if (user.TelegramUsername != telegramAuth.Username)
            {
                user.TelegramUsername = telegramAuth.Username;
                needsUpdate = true;
            }

            if (user.LanguageCode != telegramAuth.LanguageCode)
            {
                user.LanguageCode = telegramAuth.LanguageCode;
                needsUpdate = true;
            }

            if (needsUpdate)
            {
                user.UpdatedAt = DateTime.UtcNow;
                await userRepository.UpdateAsync(user);
            }
        }

        return user;
    }

    public async Task<bool> ValidateTelegramHashAsync(TelegramAuthDto telegramAuth)
    {
        if (string.IsNullOrEmpty(telegramAuth.Hash))
        {
            throw new Exception("Hash is not provided");
        }
        
        // Проверка актуальности данных аутентификации (не старше 24 часов)
        if (!long.TryParse(telegramAuth.AuthDate, out var authDate))
        {
            return false;
        }

        var authDateTime = DateTimeOffset.FromUnixTimeSeconds(authDate).UtcDateTime;
        if ((DateTime.UtcNow - authDateTime).TotalHours > 24)
        {
            return false;
        }

        // Создаем строку для проверки подписи
        var dataCheckString = new StringBuilder();
        dataCheckString.Append($"auth_date={telegramAuth.AuthDate}\n");
        dataCheckString.Append($"first_name={telegramAuth.FirstName}\n");

        if (!string.IsNullOrEmpty(telegramAuth.LastName))
        {
            dataCheckString.Append($"last_name={telegramAuth.LastName}\n");
        }

        if (!string.IsNullOrEmpty(telegramAuth.Username))
        {
            dataCheckString.Append($"username={telegramAuth.Username}\n");
        }

        if (!string.IsNullOrEmpty(telegramAuth.PhotoUrl))
        {
            dataCheckString.Append($"photo_url={telegramAuth.PhotoUrl}\n");
        }

        dataCheckString.Append($"id={telegramAuth.UserId}");

        if (string.IsNullOrEmpty(_botToken))
        {
            throw new Exception("Bot token is not configured");
        }
        
        // Получаем ключ на основе токена бота
        using var sha256 = SHA256.Create();
        var secretKey = sha256.ComputeHash(Encoding.UTF8.GetBytes(_botToken));

        // Вычисляем HMAC
        using var hmac = new HMACSHA256(secretKey);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString.ToString()));
        var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();

        // Сравниваем с переданным хешем
        return hashString == telegramAuth.Hash.ToLower();
    }

    public async Task<string> GenerateApiTokenAsync(User user)
    {
        if (string.IsNullOrEmpty(_jwtSecret))
        {
            throw new Exception("JWT secret is not configured");
        }
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim("TelegramId", user.TelegramUserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}".Trim()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7), // Токен действителен 7 дней
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}