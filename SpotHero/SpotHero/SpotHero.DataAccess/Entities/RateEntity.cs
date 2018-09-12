using System;

namespace SpotHero.DataAccess.Entities
{
	public class RateEntity
	{
		public int Id { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public DayOfWeek DayOfWeek { get; set; }
		public decimal Price { get; set; }

		public int ParkingId { get; set; }
		public ParkingEntity Parking { get; set; }
	}
}
