using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Repository.MSSQL
{
	internal class DatabaseRepository(DbContext dbContext) : IDatabaseRepository
	{
		readonly DbContext _dbContext = dbContext;

		public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, IEntity
		{
			var dbSet = _dbContext.Set<TEntity>();
			return dbSet.Where(where);
		}
		public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
		{
			_dbContext.Add(entity);
		}
		public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
		{
			_dbContext.AddRange(entities);
		}
		public async Task<int> CommitAsync()
		{
			try
			{
				var changes = _dbContext.SaveChangesAsync();
				return await changes;
			}
			catch (DbUpdateException ex)
			{
				throw new ValidationException(ex.GetBaseException().Message);
			}
		}
		public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
		{
			_dbContext.Update(entity);
		}
		public void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity
		{
			_dbContext.Remove(entity);
		}
	}
}
