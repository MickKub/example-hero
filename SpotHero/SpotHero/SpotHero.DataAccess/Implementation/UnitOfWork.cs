using Microsoft.EntityFrameworkCore.Storage;
using SpotHero.DataAccess.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IDbTransaction = SpotHero.DataAccess.Abstraction.IDbTransaction;

namespace SpotHero.DataAccess.Implementation
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly SpotHeroDbContext _dbContext;
		private bool _disposed;
		private Dictionary<string, object> _repositories;

		public UnitOfWork(SpotHeroDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IRepository<T> Repository<T>()
			where T : class
		{
			if (_repositories == null)
			{
				_repositories = new Dictionary<string, object>();
			}

			var type = typeof(T).Name;

			if (!_repositories.ContainsKey(type))
			{
				var repositoryType = typeof(Repository<>);
				var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);
				_repositories.Add(type, repositoryInstance);
			}
			return (Repository<T>)_repositories[type];
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}

		public IDbTransaction BeginTransaction()
		{
			IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();
			return new EfDbTransaction(transaction);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_dbContext.Dispose();
				}
			}
			_disposed = true;
		}
	}

	internal sealed class EfDbTransaction : IDbTransaction
	{
		private readonly IDbContextTransaction _transaction;

		internal EfDbTransaction(IDbContextTransaction transaction)
		{
			_transaction = transaction;
		}

		public void Commit() => _transaction.Commit();

		public void Rollback() => _transaction.Rollback();

		public void Dispose() => _transaction.Dispose();
	}
}
