using Chief.Domain.Entities;
using Chief.Domain.Interfaces;
using Chief.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chief.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetByTelegramIdAsync(long telegramUserId)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.TelegramUserId == telegramUserId);
    }

    public async Task<User> CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ExistsAsync(long telegramUserId)
    {
        return await context.Users.AnyAsync(u => u.TelegramUserId == telegramUserId);
    }
}