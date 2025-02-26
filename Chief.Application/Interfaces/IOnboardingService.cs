using Chief.Application.DTOs;

namespace Chief.Application.Interfaces;

public interface IOnboardingService
{
    Task<OnboardingProfileDto> GetProfileByUserIdAsync(int userId);
    Task<OnboardingProfileDto> UpdateProfileAsync(int profileId, UpdateOnboardingProfileDto dto, int userId);
}