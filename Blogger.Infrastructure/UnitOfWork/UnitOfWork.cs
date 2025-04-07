using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogger.Domain.Interfaces;
using Blogger.Infrastructure.Constants;
using Blogger.Infrastructure.UnitOfWork.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blogger.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        // Database Context
        private BloggerDbContext _context;
        // Database DbSet as repository
        private readonly ConcurrentDictionary<string, object> _repositories;
        // disposed status
        private bool _disposed = false;
        public UnitOfWork(BloggerDbContext context)
        {
            _context = context;
            _repositories = new();
        }
        public DbTransaction? CurrentTransaction { get; set; }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (CurrentTransaction == null)
            {
                throw new InvalidOperationException("No exist transaction");
            }
            try
            {
                await CurrentTransaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await RollbackAsync(cancellationToken);
                throw new Exception("Transaction commit failed. Rolled back.", ex);
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task<DbTransaction> CreateTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (CurrentTransaction != null)
            {
                throw new InvalidOperationException("No transaction exist");
            }
            var currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            CurrentTransaction = currentTransaction.GetDbTransaction();
            return CurrentTransaction;
        }

        public async Task DisposeTransactionAsync()
        {
            if (CurrentTransaction != null)
            {
                await CurrentTransaction.DisposeAsync();
                CurrentTransaction = null;
            }
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(sql, parameters);
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return _repositories.GetOrAdd(
                typeof(TEntity).Name,
                _ => new Repository<TEntity>(_context)
            ) as IRepository<TEntity> ?? throw new InvalidOperationException($"Could not create repository for {typeof(TEntity).Name}");
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (CurrentTransaction == null)
            {
                throw new InvalidOperationException("No transaction exist");
            }
            try
            {
                await CurrentTransaction.RollbackAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Transaction rollback failed.", ex);
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _repositories?.Clear();
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
