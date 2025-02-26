using Chief.Domain.Entities;

namespace Chief.Domain.Interfaces;

public interface IOnboardingRepository
{
    Task<OnboardingProfile?> GetByIdAsync(int id);
    Task<OnboardingProfile?> GetLatestProfileByUserIdAsync(int userId);
    Task<OnboardingProfile> CreateAsync(OnboardingProfile profile);
    Task<OnboardingProfile> UpdateAsync(OnboardingProfile profile);
        
    Task ClearFoodPreferencesAsync(int profileId);
    Task ClearExcludedProductsAsync(int profileId);
    Task ClearHouseholdMembersAsync(int profileId);
    Task ClearTimePreferencesAsync(int profileId);
        
    Task AddFoodPreferencesAsync(IEnumerable<FoodPreference> preferences);
    Task AddExcludedProductsAsync(IEnumerable<ExcludedProduct> products);
    Task AddHouseholdMembersAsync(IEnumerable<HouseholdMember> members);
    Task AddTimePreferencesAsync(IEnumerable<TimePreference> preferences);
}