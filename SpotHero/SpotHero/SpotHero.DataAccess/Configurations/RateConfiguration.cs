using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotHero.DataAccess.Entities;

namespace SpotHero.DataAccess.Configurations
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
