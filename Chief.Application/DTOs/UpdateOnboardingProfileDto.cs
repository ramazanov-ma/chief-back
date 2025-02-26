namespace Chief.Application.DTOs;

public class UpdateOnboardingProfileDto
{
    public int? CurrentStep { get; set; }
    public bool? Completed { get; set; }
    public List<FoodPreferenceDto>? FoodPreferences { get; set; }
    public List<ExcludedProductDto>? ExcludedProducts { get; set; }
    public List<HouseholdMemberDto>? HouseholdMembers { get; set; }
    public List<TimePreferenceDto>? TimePreferences { get; set; }
}