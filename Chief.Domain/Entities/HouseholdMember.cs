using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chief.Domain.Entities;

public class HouseholdMember
{
    [Key] public int Id { get; set; }

    [Required] public string? Name { get; set; }

    public int Age { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    [Required] public string? Gender { get; set; }

    public string? Activity { get; set; }

    public int OnboardingProfileId { get; set; }

    [ForeignKey("OnboardingProfileId")] public virtual OnboardingProfile? OnboardingProfile { get; set; }
}