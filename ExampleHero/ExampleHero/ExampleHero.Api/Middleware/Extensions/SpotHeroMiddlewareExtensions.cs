using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExampleHero.DataAccess;
using ExampleHero.DataAccess.Abstraction;
using ExampleHero.DataAccess.Implementation;
using ExampleHero.Operations.Abstraction;
using ExampleHero.Operations.Implementation;

namespace ExampleHero.Api.Middleware.Extensions
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
				services.AddDbContext<ExampleHeroDbContext>(options =>
					options.UseInMemoryDatabase(databaseName: "MemoryDb"));
			}
			else
			{
				services.AddDbContext<ExampleHeroDbContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
						optionsBuilder => optionsBuilder.MigrationsAssembly("ExampleHero.DataAccess")));
			}
		
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddTransient<IRateOperations, RateOperations>();
		}

		public static void DbSeed(this IApplicationBuilder app, IConfiguration configuration)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var dbContext = serviceScope.ServiceProvider.GetService<ExampleHeroDbContext>();
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
