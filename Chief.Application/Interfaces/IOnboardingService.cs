using Chief.Domain.Entities;

namespace Chief.Application.Interfaces
{
    public interface IOnboardingService
    {
        bool StartOnboarding(OnboardingEntity entity);
    }
}