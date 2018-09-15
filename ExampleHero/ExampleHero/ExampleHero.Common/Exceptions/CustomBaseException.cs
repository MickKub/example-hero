using System;

namespace ExampleHero.Common.Exceptions
{
	public class CustomBaseException : Exception
	{
		public CustomBaseException(string message = null, Exception innerException = null)
			: base(message, innerException)
		{ }
	}
}
