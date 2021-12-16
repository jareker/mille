using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mille.Domain.Aggregates;
using Mille.Domain.Common;

namespace TestProject1.Stubs
{
	public class RepositoryStub : IResourceRepository
	{
		private readonly List<Resource> _resources = new();

		public IUnitOfWork UnitOfWork => new UnitOfWork();
		public Task Create(Resource item)
		{
			_resources.Add(item);
			return Task.CompletedTask;;
		}

		public Task<Resource> GetById(Guid id)
		{
			return Task.FromResult(_resources.FirstOrDefault(a => a.Id == id));
		}

		public Task Delete(Resource item)
		{
			if (_resources.Contains(item))
			{
				_resources.Remove(item);
			}

			return Task.CompletedTask;
		}

		public Task Update(Resource item)
		{
			var existingItem = _resources.FirstOrDefault(a => a.Id == item.Id);

			_resources.Remove(existingItem);
			_resources.Add(item);

			return Task.CompletedTask;
		}

		public Task<IEnumerable<Resource>> GetAll()
		{
			return Task.FromResult(_resources.AsEnumerable());
		}
	}
}