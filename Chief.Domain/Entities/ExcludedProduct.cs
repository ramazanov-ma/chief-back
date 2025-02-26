using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chief.Domain.Entities;

public class ExcludedProduct
{
    [Key] public int Id { get; set; }

    [Required] public string? Name { get; set; }

    public int OnboardingProfileId { get; set; }

    [ForeignKey("OnboardingProfileId")] public virtual OnboardingProfile? OnboardingProfile { get; set; }
}