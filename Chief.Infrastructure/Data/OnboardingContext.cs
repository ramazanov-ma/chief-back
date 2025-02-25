using Chief.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chief.Infrastructure.Data
{
    public class OnboardingContext(DbContextOptions<OnboardingContext> options) : DbContext(options)
    {
        public DbSet<OnboardingEntity> Onboarding { get; set; }
    }
}