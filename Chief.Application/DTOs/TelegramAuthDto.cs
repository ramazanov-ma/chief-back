namespace Chief.Application.DTOs;

public class TelegramAuthDto
{
    public long UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? LanguageCode { get; set; }
    public string? PhotoUrl { get; set; }
    public string? AuthDate { get; set; }
    public string? Hash { get; set; }
}