using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogger.Domain.Interfaces;

namespace Blogger.Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public DbTransaction? CurrentTransaction { get; protected set; }

        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;

        Task<DbTransaction> CreateTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);

        int ExecuteSqlCommand(string sql, params object[] parameters);

        Task SaveAsync(CancellationToken cancellationToken = default);

        Task DisposeTransactionAsync();
    }
}
