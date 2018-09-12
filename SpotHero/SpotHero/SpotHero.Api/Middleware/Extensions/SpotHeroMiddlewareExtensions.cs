using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotHero.DataAccess;
using SpotHero.DataAccess.Abstraction;
using SpotHero.DataAccess.Implementation;
using SpotHero.Operations.Abstraction;
using SpotHero.Operations.Implementation;

namespace SpotHero.Api.Middleware.Extensions
{
	public static class SpotHeroMiddlewareExtensions
	{
		public static void ExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}

		public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			if (bool.TryParse(configuration.GetSection("UseMemoryDb")?.Value, out var result) && result)
			{
				services.AddDbContext<SpotHeroDbContext>(options =>
					options.UseInMemoryDatabase(databaseName: "MemoryDb"));
			}
			else
			{
				services.AddDbContext<SpotHeroDbContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
						optionsBuilder => optionsBuilder.MigrationsAssembly("SpotHero.DataAccess")));
			}
		
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddTransient<IRateOperations, RateOperations>();
		}

		public static void DbSeed(this IApplicationBuilder app, IConfiguration configuration)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var dbContext = serviceScope.ServiceProvider.GetService<SpotHeroDbContext>();
				IUnitOfWork unitOfWork;
				if (bool.TryParse(configuration.GetSection("UseMemoryDb")?.Value, out var result) && result)
				{
					unitOfWork = serviceScope.ServiceProvider.GetService<IUnitOfWork>();
				}
				else
				{
					dbContext.Database.Migrate();
					unitOfWork = serviceScope.ServiceProvider.GetService<IUnitOfWork>();
				}
				DatabaseSeedInitializer.Seed(unitOfWork).GetAwaiter().GetResult();
			}			
		}
	}
}
