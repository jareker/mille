using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mille.Domain.Aggregates;

namespace Mille.Infrastructure.Context.Configuration
{
	internal sealed class ResourceItemTypeConfiguration : IEntityTypeConfiguration<ResourceItem>
	{
		public void Configure(EntityTypeBuilder<ResourceItem> builder)
		{
			builder.HasKey(a => a.Id);
		}
	}
}