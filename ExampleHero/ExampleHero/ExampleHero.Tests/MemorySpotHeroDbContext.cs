using Microsoft.EntityFrameworkCore;
using ExampleHero.DataAccess;
using ExampleHero.DataAccess.Abstraction;
using ExampleHero.DataAccess.Implementation;

namespace ExampleHero.Tests
{
	internal static class MemorySpotHeroDbContext
	{
		private static readonly ExampleHeroDbContext MemoryDbContext;

		static MemorySpotHeroDbContext()
		{
			var optionsBuilder =
				new DbContextOptionsBuilder<ExampleHeroDbContext>().UseInMemoryDatabase(databaseName: "Add_writes_to_database");
			MemoryDbContext = new ExampleHeroDbContext(optionsBuilder.Options);
		}

		public static IUnitOfWork UnitOfWork => new UnitOfWork(MemoryDbContext);
	}
}
