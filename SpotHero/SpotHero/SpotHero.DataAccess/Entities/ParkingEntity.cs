using System.Collections.Generic;

namespace SpotHero.DataAccess.Entities
{
	public class ParkingEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public ParkingLocationEntity Location { get; set; }
		public ICollection<RateEntity> Rates { get; set; }
	}
}
