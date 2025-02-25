using Chief.Data;
using Chief.Data.Repositories;
using Chief.Services.Implementations;
using Chief.Services.Interfaces;

namespace Chief;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // services.AddDbContext<OnboardingContext>(options =>
        //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // services.AddScoped<IOnboardingService, OnboardingService>();
        // services.AddScoped<IOnboardingRepository, OnboardingRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}