using ExampleHero.DataModel;
using System;
using System.Threading.Tasks;

namespace ExampleHero.Operations.Abstraction
{
	public interface IRateOperations
	{
		Task<RateModel> GetAsync(DateTimeOffset startDateTime, DateTimeOffset endDateTime);
	}
}
