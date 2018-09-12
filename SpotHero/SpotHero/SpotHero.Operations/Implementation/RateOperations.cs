using Microsoft.EntityFrameworkCore;
using SpotHero.Common.Exceptions;
using SpotHero.DataAccess.Abstraction;
using SpotHero.DataAccess.Entities;
using SpotHero.DataModel;
using SpotHero.Operations.Abstraction;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpotHero.Operations.Implementation
{
	public class RateOperations : IRateOperations
	{
		private readonly IRepository<RateEntity> _rateRepository;

		public RateOperations(IUnitOfWork unitOfWork)
		{
			_rateRepository = unitOfWork.Repository<RateEntity>();
		}

		public async Task<RateModel> GetAsync(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
		{
			if (startDateTime.Date != endDateTime.Date || startDateTime.DateTime >= endDateTime.DateTime)
			{
				throw new CustomBaseException("INCORRECT_DATE_OR_TIME_SPAN_RANGES");
			}

			var rate = await _rateRepository.GetAsNoTracking(x => x.DayOfWeek == startDateTime.DayOfWeek
			            && x.StartTime <= startDateTime.TimeOfDay
			            && endDateTime.TimeOfDay <= x.EndTime).Select(x =>
				           new RateModel
				           {
					           Price = x.Price
				           }).FirstOrDefaultAsync() ??
			           throw new CustomBaseException("UNAVAILABLE_RATES_FOR_CURRENTS_TIME_SPANS");
			;

			return new RateModel
			{
				From = startDateTime,
				To = endDateTime,
				Price = rate.Price
			};
		}

	}
}
