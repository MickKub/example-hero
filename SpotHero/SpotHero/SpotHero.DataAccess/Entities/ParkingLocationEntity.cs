using SpotHero.Common;

namespace SpotHero.DataAccess.Entities
{
	public class ParkingLocationEntity
	{
		public ParkingLocationEntity()
		{
			Coordinates = new Coordinates();
		}

		public int Id { get; set; }
		public string Address { get; set; }
		public Coordinates Coordinates { get; set; }

		public int ParkingId { get; set; }
		public ParkingEntity Parking { get; set; }
	}
}
