using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotHero.DataAccess.Entities;

namespace SpotHero.DataAccess.Configurations
{
	public class ParkingConfiguration : IEntityTypeConfiguration<ParkingEntity>
	{
		public void Configure(EntityTypeBuilder<ParkingEntity> builder)
		{
			builder.ToTable("Parkings");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).IsRequired();
			builder.Property(x => x.Description).IsRequired();

			builder.HasOne(x => x.Location).WithOne(x => x.Parking)
				.HasForeignKey<ParkingLocationEntity>(x => x.ParkingId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(x => x.Rates).WithOne(x => x.Parking)
				.HasForeignKey(x => x.ParkingId).IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
