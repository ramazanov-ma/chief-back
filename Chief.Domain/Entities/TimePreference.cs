using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chief.Domain.Entities;

public class TimePreference
{
    [Key] public int Id { get; set; }

    [Required] public string? Day { get; set; }

    [Required] public string? Meal { get; set; }

    [Required] public string? Time { get; set; }

    public int OnboardingProfileId { get; set; }

    [ForeignKey("OnboardingProfileId")] public virtual OnboardingProfile? OnboardingProfile { get; set; }
}