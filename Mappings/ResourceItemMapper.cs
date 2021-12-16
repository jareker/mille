using Mille.Domain.Aggregates;
using Mille.Models;

namespace Mille.Mappings
{
	public static class ResourceItemMapper
	{
		public static ResourceItem FromDataModel(ResourceItemModel model)
		{
			return new ResourceItem(model.Key, model.Value);
		}
	}
}