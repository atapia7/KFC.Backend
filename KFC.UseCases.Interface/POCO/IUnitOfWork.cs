
namespace KFC.UseCases.Interface;

public interface IUnitOfWork
{
	Task<int> SaveChanges();
}