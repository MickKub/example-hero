using System;

namespace SpotHero.DataAccess.Abstraction
{
	public interface IDbTransaction : IDisposable
	{
		void Commit();
		void Rollback();
	}
}
