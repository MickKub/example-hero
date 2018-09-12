using NUnit.Framework;
using SpotHero.DataAccess;
using SpotHero.DataAccess.Abstraction;
using SpotHero.DataAccess.Entities;
using SpotHero.Operations.Abstraction;
using SpotHero.Operations.Implementation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SpotHero.Common.Exceptions;

namespace SpotHero.Tests
{
	[TestFixture]
	public class RateOperationsTest
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRateOperations _rateOperations;

		public RateOperationsTest()
		{
			_unitOfWork = MemorySpotHeroDbContext.UnitOfWork;
			_rateOperations = new RateOperations(_unitOfWork);
		}

		[OneTimeSetUp]
		public async Task TestInitializer()
		{
			var parkingRepo = _unitOfWork.Repository<ParkingEntity>();
			var parking = DatabaseSeedInitializer.Parking;
			parking.Rates = DatabaseSeedInitializer.Rates;
			await parkingRepo.InsertAsync(parking);
		}

		[Test]
		//Mon, Tue, Thu, Fri, Sat, Sun; 9.00-21.00
		[TestCase("2018-09-10T10:03:09Z", "2018-09-10T19:03:09Z", 1500)]
		[TestCase("2018-09-11T10:03:09Z", "2018-09-11T19:03:09Z", 1500)]
		[TestCase("2018-09-13T10:03:09Z", "2018-09-13T19:03:09Z", 1500)]
		[TestCase("2018-09-14T10:03:09Z", "2018-09-14T19:03:09Z", 1500)]
		[TestCase("2018-09-15T10:03:09Z", "2018-09-15T19:03:09Z", 1500)]
		[TestCase("2018-09-16T10:03:09Z", "2018-09-16T19:03:09Z", 1500)]
		//Wed; 6.00-18.00
		[TestCase("2018-09-12T10:03:09Z", "2018-09-12T17:03:09Z", 1750)]
		//Mon, Wed, Sat; 1.00-5.00
		[TestCase("2018-09-10T02:03:09Z", "2018-09-10T03:03:09Z", 1000)]
		[TestCase("2018-09-12T02:03:09Z", "2018-09-12T03:03:09Z", 1000)]
		[TestCase("2018-09-15T02:03:09Z", "2018-09-15T04:03:09Z", 1000)]
		//Sun, Tue; 1.00-7.00
		[TestCase("2018-09-11T02:03:09Z", "2018-09-11T06:03:09Z", 925)]
		[TestCase("2018-09-16T02:03:09Z", "2018-09-16T06:03:09Z", 925)]
		public void Get_WithValidTimeSpans_ShouldReturnRate(string startDateTimeString, string endDateTimeString, decimal expectedRate)
		{
			AssertFailIfNotSucceedParse(startDateTimeString, endDateTimeString, out var startTime, out var endTime);

			var rate = _rateOperations.GetAsync(startTime, endTime).GetAwaiter().GetResult();

			Assert.AreEqual(expectedRate, rate.Price);
		}

		[Test]
		//Mon, Tue, Thu, Fri, Sat, Sun; 9.00-21.00
		[TestCase("2018-09-10T07:03:09Z", "2018-09-10T19:03:09Z")]
		[TestCase("2018-09-11T10:03:09Z", "2018-09-11T22:03:09Z")]
		[TestCase("2018-09-13T10:03:09Z", "2018-09-13T22:03:09Z")]
		[TestCase("2018-09-14T10:03:09Z", "2018-09-14T22:03:09Z")]
		[TestCase("2018-09-15T10:03:09Z", "2018-09-15T22:03:09Z")]
		[TestCase("2018-09-16T10:03:09Z", "2018-19-17T21:03:09Z")]
		//Wed; 6.00-18.00
		[TestCase("2018-09-12T10:03:09Z", "2018-09-22T17:03:09Z")]
		//Mon, Wed, Sat; 1.00-5.00
		[TestCase("2018-09-10T02:03:09Z", "2018-09-12T13:03:09Z")]
		[TestCase("2018-09-12T02:03:09Z", "2018-09-13T03:03:09Z")]
		[TestCase("2018-09-15T02:03:09Z", "2018-09-16T04:03:09Z")]
		//Sun, Tue; 1.00-7.00
		[TestCase("2018-09-11T02:03:09Z", "2018-09-11T09:03:09Z")]
		[TestCase("2018-09-16T02:03:09Z", "2018-09-19T06:03:09Z")]
		public void Get_WithInvalidTimeSpans_ShouldReturnException(string startDateTimeString, string endDateTimeString)
		{
			 AssertFailIfNotSucceedParse(startDateTimeString, endDateTimeString, out var startTime, out var endTime);

			Assert.Throws<CustomBaseException>(() =>
				_rateOperations.GetAsync(startTime, endTime).GetAwaiter().GetResult());
		}

		private static void AssertFailIfNotSucceedParse(string startDateTimeString, string endDateTimeString, out DateTimeOffset startDateTime, out DateTimeOffset endDateTime)
		{
			if (!DateTimeOffset.TryParse(startDateTimeString, null, System.Globalization.DateTimeStyles.RoundtripKind,
				    out startDateTime) &
			    !DateTimeOffset.TryParse(endDateTimeString, null, System.Globalization.DateTimeStyles.RoundtripKind,
				    out endDateTime))
			{
				Debugger.Log(0, string.Empty, "DateTime parse error");
				Assert.Fail();
			}
		}

	}
}
