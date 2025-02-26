using AutoMapper;
using Chief.Application.DTOs;
using Chief.Application.Interfaces;
using Chief.Domain.Entities;
using Chief.Domain.Interfaces;

namespace Chief.Application.Services
{
    public class OnboardingService(
        IOnboardingRepository onboardingRepository,
        IUserRepository userRepository,
        IMapper mapper)
        : IOnboardingService
    {
        public async Task<OnboardingProfileDto> GetProfileByUserIdAsync(int userId)
        {
            var user = await userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            var profile = await onboardingRepository.GetLatestProfileByUserIdAsync(userId);

            if (profile == null)
            {
                // Создаем новый профиль онбординга для пользователя
                profile = new OnboardingProfile
                {
                    UserId = userId,
                    CurrentStep = 1,
                    Completed = false,
                    CreatedAt = DateTime.UtcNow
                };

                profile = await onboardingRepository.CreateAsync(profile);
            }

            return mapper.Map<OnboardingProfileDto>(profile);
        }

        public async Task<OnboardingProfileDto> UpdateProfileAsync(int profileId, UpdateOnboardingProfileDto dto,
            int userId)
        {
            var profile = await onboardingRepository.GetByIdAsync(profileId);

            if (profile == null)
            {
                throw new KeyNotFoundException($"Onboarding profile with ID {profileId} not found");
            }

            if (profile.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this profile");
            }

            // Обновляем базовые поля используя AutoMapper
            mapper.Map(dto, profile);
            profile.UpdatedAt = DateTime.UtcNow;

            // Обновляем пищевые предпочтения
            if (dto.FoodPreferences != null)
            {
                await onboardingRepository.ClearFoodPreferencesAsync(profileId);

                var newPreferences = mapper.Map<IEnumerable<FoodPreference>>(dto.FoodPreferences)
                    .Select(fp =>
                    {
                        fp.OnboardingProfileId = profileId;
                        return fp;
                    })
                    .ToList();

                await onboardingRepository.AddFoodPreferencesAsync(newPreferences);
                profile.FoodPreferences = newPreferences;
            }

            // Обновляем исключенные продукты
            if (dto.ExcludedProducts != null)
            {
                await onboardingRepository.ClearExcludedProductsAsync(profileId);

                var newProducts = mapper.Map<IEnumerable<ExcludedProduct>>(dto.ExcludedProducts)
                    .Select(ep =>
                    {
                        ep.OnboardingProfileId = profileId;
                        return ep;
                    })
                    .ToList();

                await onboardingRepository.AddExcludedProductsAsync(newProducts);
                profile.ExcludedProducts = newProducts;
            }

            // Обновляем членов семьи
            if (dto.HouseholdMembers != null)
            {
                await onboardingRepository.ClearHouseholdMembersAsync(profileId);

                var newMembers = mapper.Map<IEnumerable<HouseholdMember>>(dto.HouseholdMembers)
                    .Select(hm =>
                    {
                        hm.OnboardingProfileId = profileId;
                        return hm;
                    })
                    .ToList();

                await onboardingRepository.AddHouseholdMembersAsync(newMembers);
                profile.HouseholdMembers = newMembers;
            }

            // Обновляем временные предпочтения
            if (dto.TimePreferences != null)
            {
                await onboardingRepository.ClearTimePreferencesAsync(profileId);

                var newPreferences = mapper.Map<IEnumerable<TimePreference>>(dto.TimePreferences)
                    .Select(tp =>
                    {
                        tp.OnboardingProfileId = profileId;
                        return tp;
                    })
                    .ToList();

                await onboardingRepository.AddTimePreferencesAsync(newPreferences);
                profile.TimePreferences = newPreferences;
            }

            await onboardingRepository.UpdateAsync(profile);

            return mapper.Map<OnboardingProfileDto>(profile);
        }
    }
}