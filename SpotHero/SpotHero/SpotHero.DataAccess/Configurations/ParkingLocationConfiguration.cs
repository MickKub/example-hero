using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotHero.DataAccess.Entities;

namespace SpotHero.DataAccess.Configurations
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
