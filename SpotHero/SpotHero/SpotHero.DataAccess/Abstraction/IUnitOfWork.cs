using System;
using System.Threading.Tasks;

namespace SpotHero.DataAccess.Abstraction
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<T> Repository<T>()
			where T : class;

		Task SaveChangesAsync();

		IDbTransaction BeginTransaction();
	}
}
