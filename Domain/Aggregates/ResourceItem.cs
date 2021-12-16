using Mille.Domain.Common;

namespace Mille.Domain.Aggregates
{
	public class ResourceItem : Entity
	{
		public string Key { get; private set; }
		public string Value { get; private set; }

		protected ResourceItem(){}
		
		public ResourceItem(string key, string value)
		{
			Key = key;
			Value = value;
		}
	}
}