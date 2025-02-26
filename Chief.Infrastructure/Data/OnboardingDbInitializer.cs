using Chief.Domain.Entities;

namespace Chief.Infrastructure.Data
{
    public static class OnboardingDbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Добавьте начальные данные, если необходимо
        }
    }
}