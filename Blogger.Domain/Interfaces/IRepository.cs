using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<TEntity?> FindAsync(int id, CancellationToken cancellationToken);
        public IQueryable<TEntity> OrderBy<TProperty>(Expression<Func<TEntity, TProperty>> predicate);
        // Select * from DbSet<TEntity> Offset-limit/Offset-fetch/Skip-take/....
        public IQueryable<TEntity> SkipTake(int skip, int take);
        public IQueryable<TEntity> SkipTakeOrderBy<TProperty>(Expression<Func<TEntity, TProperty>> predicate, int skip, int take);
        // Select * from DbSet<TEntity> where 
        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        // Get queryable DbSet<TEntity>
        public IQueryable<TEntity> QueryableEntities();
        public Task<IList<TEntity>> GetListAsync(CancellationToken cancellationToken);
        public int Count();
        public Task InsertAsync(CancellationToken cancellationToken, TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public void UpdateRange(IEnumerable<TEntity> entities);
        public void DeleteRange(IEnumerable<TEntity> entities);
        public void DeleteRange(Expression<Func<TEntity, bool>> predicate);
        public Task InsertRangeAsync(CancellationToken cancellationToken, IEnumerable<TEntity> entities);
    }
}
