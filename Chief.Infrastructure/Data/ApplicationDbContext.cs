using Chief.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chief.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OnboardingProfile> OnboardingProfiles { get; set; }
        public DbSet<FoodPreference> FoodPreferences { get; set; }
        public DbSet<ExcludedProduct> ExcludedProducts { get; set; }
        public DbSet<HouseholdMember> HouseholdMembers { get; set; }
        public DbSet<TimePreference> TimePreferences { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<OnboardingProfile>()
                .HasOne(p => p.User)
                .WithMany(u => u.OnboardingProfiles)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<FoodPreference>()
                .HasOne(fp => fp.OnboardingProfile)
                .WithMany(op => op.FoodPreferences)
                .HasForeignKey(fp => fp.OnboardingProfileId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<ExcludedProduct>()
                .HasOne(ep => ep.OnboardingProfile)
                .WithMany(op => op.ExcludedProducts)
                .HasForeignKey(ep => ep.OnboardingProfileId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<HouseholdMember>()
                .HasOne(hm => hm.OnboardingProfile)
                .WithMany(op => op.HouseholdMembers)
                .HasForeignKey(hm => hm.OnboardingProfileId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<TimePreference>()
                .HasOne(tp => tp.OnboardingProfile)
                .WithMany(op => op.TimePreferences)
                .HasForeignKey(tp => tp.OnboardingProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}