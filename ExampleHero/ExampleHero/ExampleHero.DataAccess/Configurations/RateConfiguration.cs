using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExampleHero.DataAccess.Entities;

namespace ExampleHero.DataAccess.Configurations
{
	public class RateConfiguration : IEntityTypeConfiguration<RateEntity>
	{
		public void Configure(EntityTypeBuilder<RateEntity> builder)
		{
			builder.ToTable("Rates");

			builder.HasKey(x => x.Id);
		}
	}
}
