namespace KFC.UseCases.Query;

public abstract class QueryFilter<TEntity>
{
	public abstract IQueryable<TEntity> Apply(IQueryable<TEntity> queryable);
}