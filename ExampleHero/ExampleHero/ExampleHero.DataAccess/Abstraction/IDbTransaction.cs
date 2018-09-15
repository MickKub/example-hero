using System;

namespace ExampleHero.DataAccess.Abstraction
{
	public interface IDbTransaction : IDisposable
	{
		void Commit();
		void Rollback();
	}
}
