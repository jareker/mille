using System;
using System.Threading.Tasks;
using Mille.Domain.Aggregates;

namespace Mille.Domain.Common
{
	public interface IRepository<T>  where T : IAggregateRoot
	{
		IUnitOfWork UnitOfWork { get; }
		
		Task Create(T item);

		Task<T> GetById(Guid id);

		Task Delete(T item);
		Task Update(T item);
	}
}