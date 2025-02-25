using Chief.Domain.Entities;
using Chief.Domain.Interfaces;
using Chief.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chief.Infrastructure.Repositories
{
    public class OnboardingRepository : IOnboardingRepository
    {
        private readonly OnboardingContext _context;

        public OnboardingRepository(OnboardingContext context)
        {
            _context = context;
        }

        public void Add(OnboardingEntity entity)
        {
            _context.Onboarding.Add(entity);
        }

        public OnboardingEntity Get(int id)
        {
            return _context.Onboarding.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}