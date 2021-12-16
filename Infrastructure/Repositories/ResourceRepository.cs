using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mille.Domain.Aggregates;
using Mille.Domain.Common;
using Mille.Infrastructure.Context;

namespace Mille.Infrastructure.Repositories
{
	public sealed class ResourceRepository : IResourceRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public ResourceRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IUnitOfWork UnitOfWork => _dbContext;
		
		
		public async Task Create(Resource item)
		{
			await _dbContext.Resources.AddAsync(item);
		}

		public async Task<IEnumerable<Resource>> GetAll()
		{
			var resources = _dbContext
				.Resources
				.AsNoTracking()
				.Include(a=>a.Items);

			return await Task.FromResult(resources);
		}
		
		public async Task<Resource> GetById(Guid id)
		{
			var resource = await _dbContext.Resources.Include(a => a.Items)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (resource == null)
			{
				resource = _dbContext.Resources.Local.FirstOrDefault(a => a.Id == id);
			}

			if (resource != null)
			{
				await _dbContext.Entry(resource)
					.Collection(a => a.Items)
					.LoadAsync();
			}

			return resource;
		}

		public Task Delete(Resource item)
		{
			_dbContext.Entry(item).State = EntityState.Deleted;
			return Task.CompletedTask;
		}

		public Task Update(Resource item)
		{
			_dbContext.Entry(item).State = EntityState.Modified;
			return Task.CompletedTask;
		}

	
	}
}