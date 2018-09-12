using System;

namespace SpotHero.Common.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTimeOffset GetNextWeekday(this DateTimeOffset start, DayOfWeek day)
		{
			var daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
			return start.AddDays(daysToAdd);
		}
	}
}
