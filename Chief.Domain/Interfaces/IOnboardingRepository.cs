using Chief.Domain.Entities;

namespace Chief.Domain.Interfaces;

public interface IOnboardingRepository
{
    void Add(OnboardingEntity entity);
    OnboardingEntity Get(int id);
    void SaveChanges();
}