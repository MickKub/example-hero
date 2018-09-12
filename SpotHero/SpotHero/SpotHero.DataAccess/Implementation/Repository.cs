using Microsoft.EntityFrameworkCore;
using SpotHero.DataAccess.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpotHero.DataAccess
{
	public sealed class Repository<T> : IRepository<T>
		where T : class
	{
		private readonly SpotHeroDbContext _dbContext;
		private readonly DbSet<T> _entities;

		public Repository(SpotHeroDbContext dbContext)
		{
			_dbContext = dbContext;
			_entities = _dbContext.Set<T>();
		}

		#region sync methods

		public IQueryable<T> All => _entities;

		public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
		{
			return predicate != null ? _entities.Where(predicate) : _entities;
		}

		public IQueryable<T> GetAsNoTracking(Expression<Func<T, bool>> predicate = null)
		{
			return Get(predicate).AsNoTracking();
		}

		public IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include)
		{
			IQueryable<T> entities = _entities;
			return include.Aggregate(entities, (current, inc) => current.Include(inc)).Where(predicate);
		}

		public IQueryable<T> GetWithIncludeAsNoTracking(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include)
		{
			return GetWithInclude(predicate, include).AsNoTracking();
		}

		public IQueryable<T> Include(params Expression<Func<T, object>>[] include)
		{
			IQueryable<T> entities = _entities;
			return include.Aggregate(entities, (current, inc) => current.Include(inc));
		}

		public IQueryable<T> IncludeAsNoTracking(params Expression<Func<T, object>>[] include)
		{
			return Include(include).AsNoTracking();
		}

		public async Task<T> FindAsync(params object[] ids)
		{
			return await _entities?.FindAsync(ids);
		}

		public async Task<T> InsertAsync(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			_entities.Add(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		#endregion

		#region async methods

		public async Task<IEnumerable<T>> InsertRangeAsync(IReadOnlyCollection<T> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException(nameof(entities));
			}
			_entities.AddRange(entities);
			await _dbContext.SaveChangesAsync();
			return entities;
		}

		public async Task<T> UpdateAsync(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			_entities.Update(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			_dbContext.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _entities.FindAsync(id);

			if (entity != null)
			{
				_dbContext.Remove(entity);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task DeleteRangeAsync(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException(nameof(entities));
			}
			_dbContext.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		#endregion
	}
}
