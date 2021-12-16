using System.Collections.Generic;
using System.Threading.Tasks;
using Mille.Domain.Common;

namespace Mille.Domain.Aggregates
{
	public interface IResourceRepository : IRepository<Resource>
	{
		Task<IEnumerable<Resource>> GetAll();
	}
}