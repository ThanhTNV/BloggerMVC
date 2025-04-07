using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blogger.Infrastructure.Constants;
using Blogger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.UnitOfWork.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private BloggerDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(BloggerDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public IQueryable<TEntity> OrderBy<TProperty>(Expression<Func<TEntity, TProperty>> predicate)
        {
            return _dbSet.OrderBy(predicate);
        }
        public async Task<TEntity?> FindAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }
        public int Count()
        {
            return _dbSet.Count();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public IQueryable<TEntity> SkipTakeOrderBy<TProperty>(Expression<Func<TEntity, TProperty>> predicate, int skip, int take)
        {
            return _dbSet.OrderBy(predicate).Skip(skip).Take(take);
        }
        public async Task<IList<TEntity>> GetListAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task InsertAsync(CancellationToken cancellationToken, TEntity entity)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public IQueryable<TEntity> QueryableEntities()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> SkipTake(int skip, int take)
        {
            return _dbSet.Skip(skip).Take(take);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        public void DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate);
            _dbSet.RemoveRange(entities);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public async Task InsertRangeAsync(CancellationToken cancellationToken, IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }
    }
}
