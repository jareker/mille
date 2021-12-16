using System.Threading;
using System.Threading.Tasks;
using Mille.Domain.Common;

namespace TestProject1.Stubs
{
	public class UnitOfWork : IUnitOfWork
	{
		public void Dispose()
		{
			return;
		}

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return Task.FromResult(default(int));
		}
	}
}