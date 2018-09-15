using System;

namespace ExampleHero.DataModel
{
    public class RateModel
	{
		public DateTimeOffset From { get; set; }
	    public DateTimeOffset To { get; set; }
		public decimal Price { get; set; }
	}
}
