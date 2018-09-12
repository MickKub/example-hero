using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace SpotHero.DataAccess
{
	public sealed class SpotHeroDbContext : DbContext
	{
		public new DbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}

		public SpotHeroDbContext(DbContextOptions<SpotHeroDbContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SetEntityTypeConfigurations(ref modelBuilder);
			base.OnModelCreating(modelBuilder);
		}

		private static void SetEntityTypeConfigurations(ref ModelBuilder modelBuilder)
		{
			var typeOfEntityConfiguration = typeof(IEntityTypeConfiguration<>);

			var instances = Assembly.GetExecutingAssembly().GetTypes()
				.Where(type => !string.IsNullOrEmpty(type.Namespace) &&
								!type.IsAbstract &&
								type.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeOfEntityConfiguration)
				)
				.Select(Activator.CreateInstance).ToList();

			foreach (var instance in instances)
			{
				modelBuilder.ApplyConfiguration((dynamic)instance);
			}
		}
	}
}
