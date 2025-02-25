namespace Chief.Domain.Entities;

public sealed class OnboardingEntity
{
    public int Id { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
}