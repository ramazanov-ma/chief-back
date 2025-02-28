using Chief.Domain.Entities;

namespace Chief.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByTelegramIdAsync(long telegramUserId);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> ExistsAsync(long telegramUserId);
}   