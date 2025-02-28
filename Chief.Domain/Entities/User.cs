namespace Chief.Domain.Entities;

public class User
{
    public int Id { get; set; }
    
    // Telegram-специфичные поля
    public long TelegramUserId { get; set; }  // ID пользователя в системе Telegram
    public string? TelegramUsername { get; set; } // @username (опционально)
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? LanguageCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Дополнительные поля, которые вы можете запросить
    public int? Age { get; set; }
    
    public virtual ICollection<OnboardingProfile>? OnboardingProfiles { get; set; }
}