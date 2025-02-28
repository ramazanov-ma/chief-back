using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chief.Domain.Entities;

public class OnboardingProfile
{
    [Key] public int Id { get; set; }

    public int CurrentStep { get; set; } = 1;

    public bool Completed { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")] public virtual User? User { get; set; }

    public virtual ICollection<FoodPreference> FoodPreferences { get; set; } = new List<FoodPreference>();

    public virtual ICollection<ExcludedProduct> ExcludedProducts { get; set; } = new List<ExcludedProduct>();

    public virtual ICollection<HouseholdMember> HouseholdMembers { get; set; } = new List<HouseholdMember>();

    public virtual ICollection<TimePreference> TimePreferences { get; set; } = new List<TimePreference>();
}