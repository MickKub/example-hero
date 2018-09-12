using System;

namespace SpotHero.Common.Exceptions
{
	public class CustomBaseException : Exception
	{
		public CustomBaseException(string message = null, Exception innerException = null)
			: base(message, innerException)
		{ }
	}
}
