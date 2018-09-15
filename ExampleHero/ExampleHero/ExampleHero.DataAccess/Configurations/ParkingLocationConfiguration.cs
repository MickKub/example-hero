using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExampleHero.DataAccess.Entities;

namespace ExampleHero.DataAccess.Configurations
{
	public class ParkingLocationConfiguration : IEntityTypeConfiguration<ParkingLocationEntity>
	{
		public void Configure(EntityTypeBuilder<ParkingLocationEntity> builder)
		{
			builder.ToTable("ParkingLocations");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Address).IsRequired();
			builder.OwnsOne(x => x.Coordinates);
		}
	}
}
