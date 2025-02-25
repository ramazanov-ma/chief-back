using Chief.Application.Interfaces;
using Chief.Domain.Entities;
using Chief.Domain.Interfaces;

namespace Chief.Application.Services
{
    public class OnboardingService(IOnboardingRepository onboardingRepository) : IOnboardingService
    {
        public bool StartOnboarding(OnboardingEntity entity)
        {
            onboardingRepository.Add(entity);
            onboardingRepository.SaveChanges();
            return true;
        }
    }
}