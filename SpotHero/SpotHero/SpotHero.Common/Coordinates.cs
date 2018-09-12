namespace SpotHero.Common
{
	public class Coordinates
	{
		public Coordinates() { }

		public Coordinates(double latitude, double longitude)
		{
			Longitude = longitude;
			Latitude = latitude;
		}

		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}
}
