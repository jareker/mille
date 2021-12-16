using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mille.Domain.Aggregates;
using Mille.Domain.Common;
using Mille.Infrastructure.Context.Configuration;

namespace Mille.Infrastructure.Context
{
	public class ApplicationDbContext  : DbContext, IUnitOfWork
	{
		
		public DbSet<Resource> Resources { get; set; }
		public DbSet<ResourceItem> ResourceItems { get; set; }

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
				
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ResourceTypeConfiguration());
			modelBuilder.ApplyConfiguration(new ResourceItemTypeConfiguration());
		}
	}
}