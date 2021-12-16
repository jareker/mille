using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mille.Domain.Aggregates;

namespace Mille.Infrastructure.Context.Configuration
{
	internal sealed class ResourceTypeConfiguration : IEntityTypeConfiguration<Resource>
	{
		public void Configure(EntityTypeBuilder<Resource> builder)
		{

			builder.HasKey(a => a.Id);
			builder.Property(a => a.Name)
				.IsRequired();
			var resourceItemsNavigationProperty = builder.Metadata.FindNavigation(nameof(Resource.Items));
			resourceItemsNavigationProperty.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}