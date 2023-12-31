using System.Linq.Expressions;

namespace Repository.MSSQL
{
	public interface IDatabaseRepository
	{
		IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, IEntity;
		void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
		void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
		void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
		void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity;
		Task<int> CommitAsync();
	}
}
