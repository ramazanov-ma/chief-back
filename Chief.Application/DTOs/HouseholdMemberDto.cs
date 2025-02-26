namespace Chief.Application.DTOs;

public sealed class HouseholdMemberDto
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public string? Gender { get; set; }
    public string? Activity { get; set; }
}