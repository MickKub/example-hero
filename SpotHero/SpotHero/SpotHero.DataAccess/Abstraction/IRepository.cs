using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpotHero.DataAccess.Abstraction
{
	public interface IRepository<T> where T : class
	{
		IQueryable<T> All { get; }
		IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
		IQueryable<T> GetAsNoTracking(Expression<Func<T, bool>> predicate = null);
		IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);
		IQueryable<T> GetWithIncludeAsNoTracking(Expression<Func<T, bool>> predicate,
			params Expression<Func<T, object>>[] include);
		IQueryable<T> Include(params Expression<Func<T, object>>[] include);

		Task<T> FindAsync(params object[] ids);
		Task<T> InsertAsync(T entity);
		Task<IEnumerable<T>> InsertRangeAsync(IReadOnlyCollection<T> entities);
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task DeleteAsync(int id);
		Task DeleteRangeAsync(IEnumerable<T> entities);
	}
}
