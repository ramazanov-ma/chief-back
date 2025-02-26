using Chief.Domain.Entities;
using Chief.Domain.Interfaces;
using Chief.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chief.Infrastructure.Repositories
{
    public class OnboardingRepository(ApplicationDbContext context) : IOnboardingRepository
    {
        public async Task<OnboardingProfile?> GetByIdAsync(int id)
        {
            return await context.OnboardingProfiles
                .Include(p => p.FoodPreferences)
                .Include(p => p.ExcludedProducts)
                .Include(p => p.HouseholdMembers)
                .Include(p => p.TimePreferences)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<OnboardingProfile?> GetLatestProfileByUserIdAsync(int userId)
        {
            return await context.OnboardingProfiles
                .Include(p => p.FoodPreferences)
                .Include(p => p.ExcludedProducts)
                .Include(p => p.HouseholdMembers)
                .Include(p => p.TimePreferences)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<OnboardingProfile> CreateAsync(OnboardingProfile profile)
        {
            context.OnboardingProfiles.Add(profile);
            await context.SaveChangesAsync();
            return profile;
        }

        public async Task<OnboardingProfile> UpdateAsync(OnboardingProfile profile)
        {
            context.Entry(profile).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return profile;
        }

        public async Task ClearFoodPreferencesAsync(int profileId)
        {
            var preferences = await context.FoodPreferences
                .Where(fp => fp.OnboardingProfileId == profileId)
                .ToListAsync();

            context.FoodPreferences.RemoveRange(preferences);
            await context.SaveChangesAsync();
        }

        public async Task ClearExcludedProductsAsync(int profileId)
        {
            var products = await context.ExcludedProducts
                .Where(ep => ep.OnboardingProfileId == profileId)
                .ToListAsync();

            context.ExcludedProducts.RemoveRange(products);
            await context.SaveChangesAsync();
        }

        public async Task ClearHouseholdMembersAsync(int profileId)
        {
            var members = await context.HouseholdMembers
                .Where(hm => hm.OnboardingProfileId == profileId)
                .ToListAsync();

            context.HouseholdMembers.RemoveRange(members);
            await context.SaveChangesAsync();
        }

        public async Task ClearTimePreferencesAsync(int profileId)
        {
            var preferences = await context.TimePreferences
                .Where(tp => tp.OnboardingProfileId == profileId)
                .ToListAsync();

            context.TimePreferences.RemoveRange(preferences);
            await context.SaveChangesAsync();
        }

        public async Task AddFoodPreferencesAsync(IEnumerable<FoodPreference> preferences)
        {
            await context.FoodPreferences.AddRangeAsync(preferences);
            await context.SaveChangesAsync();
        }

        public async Task AddExcludedProductsAsync(IEnumerable<ExcludedProduct> products)
        {
            await context.ExcludedProducts.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

        public async Task AddHouseholdMembersAsync(IEnumerable<HouseholdMember> members)
        {
            await context.HouseholdMembers.AddRangeAsync(members);
            await context.SaveChangesAsync();
        }

        public async Task AddTimePreferencesAsync(IEnumerable<TimePreference> preferences)
        {
            await context.TimePreferences.AddRangeAsync(preferences);
            await context.SaveChangesAsync();
        }
    }
}