using KFC.UseCases.Interface;

namespace KFC.Gateways.SQLite;

public class UnitOfWork : IUnitOfWork
{
	private ApplicationContext _context;

	public UnitOfWork(ApplicationContext context)
	{
		_context = context;
	}

	public Task<int> SaveChanges()
	{
		return _context.SaveChangesAsync();
	}
}