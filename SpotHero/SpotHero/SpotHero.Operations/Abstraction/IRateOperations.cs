using SpotHero.DataModel;
using System;
using System.Threading.Tasks;

namespace SpotHero.Operations.Abstraction
{
	public interface IRateOperations
	{
		Task<RateModel> GetAsync(DateTimeOffset startDateTime, DateTimeOffset endDateTime);
	}
}
