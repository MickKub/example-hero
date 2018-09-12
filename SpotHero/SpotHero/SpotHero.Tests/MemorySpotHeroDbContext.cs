using Microsoft.EntityFrameworkCore;
using SpotHero.DataAccess;
using SpotHero.DataAccess.Abstraction;
using SpotHero.DataAccess.Implementation;

namespace SpotHero.Tests
{
	internal static class MemorySpotHeroDbContext
	{
		private static readonly SpotHeroDbContext MemoryDbContext;

		static MemorySpotHeroDbContext()
		{
			var optionsBuilder =
				new DbContextOptionsBuilder<SpotHeroDbContext>().UseInMemoryDatabase(databaseName: "Add_writes_to_database");
			MemoryDbContext = new SpotHeroDbContext(optionsBuilder.Options);
		}

		public static IUnitOfWork UnitOfWork => new UnitOfWork(MemoryDbContext);
	}
}
