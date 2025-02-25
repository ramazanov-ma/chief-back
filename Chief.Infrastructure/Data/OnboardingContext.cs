using Chief.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chief.Infrastructure.Data
{
    public class OnboardingContext : DbContext
    {
        public OnboardingContext(DbContextOptions<OnboardingContext> options)
            : base(options)
        {
        }

        public DbSet<OnboardingEntity> Onboarding { get; set; }
    }
}