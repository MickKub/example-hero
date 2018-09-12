using Microsoft.EntityFrameworkCore.Internal;
using SpotHero.Common;
using SpotHero.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotHero.DataAccess.Abstraction;

namespace SpotHero.DataAccess
{
	public static class DatabaseSeedInitializer
	{
		public static async Task Seed(IUnitOfWork unitOfWork)
		{
			var parkingRepo = unitOfWork.Repository<ParkingEntity>();
			if (!parkingRepo.All.Any())
			{
				var parkingEntity = Parking;
				parkingEntity.Rates = Rates;
				await parkingRepo.InsertAsync(parkingEntity);
			}
		}

		public static ParkingEntity Parking =>
			new ParkingEntity
			{
				Name = "An amazing parking",
				Description = "An amazing parking description",
				Location = new ParkingLocationEntity
				{
					Address = "San Francisco",
					Coordinates = new Coordinates(37.7749, 122.4194)
				}
			};

		public static ICollection<RateEntity> Rates =>
			new List<RateEntity>
			{
				new RateEntity
				{
					DayOfWeek = DayOfWeek.Monday,
					StartTime = new TimeSpan(9, 0, 0),
					EndTime = new TimeSpan(21, 0, 0),
					Price = 1500
				},
				new RateEntity
				{
					DayOfWeek = DayOfWeek.Tuesday,
					StartTime = new TimeSpan(9, 0, 0),
					EndTime = new TimeSpan(21, 0, 0),
					Price = 1500
				},
				new RateEntity
				{
					DayOfWeek = DayOfWeek.Thursday,
					StartTime = new TimeSpan(9, 0, 0),
					EndTime = new TimeSpan(21, 0, 0),
					Price = 1500
				},

				new RateEntity
				{
					DayOfWeek = DayOfWeek.Friday,
					StartTime = new TimeSpan(9, 0, 0),
					EndTime = new TimeSpan(21, 0, 0),
					Price = 1500
				},
				new RateEntity
				{
					DayOfWeek = DayOfWeek.Saturday,
					StartTime = new TimeSpan(9, 0, 0),
					EndTime = new TimeSpan(21, 0, 0),
					Price = 1500
				},
				new RateEntity
				{
					DayOfWeek = DayOfWeek.Sunday,
					StartTime = new TimeSpan(9, 0, 0),
					EndTime = new TimeSpan(21, 0, 0),
					Price = 1500
				},

				new RateEntity
				{
					DayOfWeek = DayOfWeek.Wednesday,
					StartTime = new TimeSpan(6, 0, 0),
					EndTime = new TimeSpan(18, 0, 0),
					Price = 1750
				},

				new RateEntity
				{
					DayOfWeek = DayOfWeek.Monday,
					StartTime = new TimeSpan(1, 0, 0),
					EndTime = new TimeSpan(5, 0, 0),
					Price = 1000
				},
				new RateEntity
				{
					DayOfWeek = DayOfWeek.Wednesday,
					StartTime = new TimeSpan(1, 0, 0),
					EndTime = new TimeSpan(5, 0, 0),
					Price = 1000
				},
				new RateEntity
				{
					DayOfWeek = DayOfWeek.Saturday,
					StartTime = new TimeSpan(1, 0, 0),
					EndTime = new TimeSpan(5, 0, 0),
					Price = 1000
				},

				new RateEntity
				{
					DayOfWeek = DayOfWeek.Sunday,
					StartTime = new TimeSpan(1, 0, 0),
					EndTime = new TimeSpan(7, 0, 0),
					Price = 925
				},
				new RateEntity
				{
					DayOfWeek = DayOfWeek.Tuesday,
					StartTime = new TimeSpan(1, 0, 0),
					EndTime = new TimeSpan(7, 0, 0),
					Price = 925
				}
			};
	}
}
