using System;
using System.Collections.Generic;
using Mille.Domain.Common;

namespace Mille.Domain.Aggregates
{
	public class Resource : Entity, IAggregateRoot
	{
		private readonly List<ResourceItem> _items = new();

		public string Name { get; private set; }
		
		public IReadOnlyCollection<ResourceItem> Items => _items.AsReadOnly();

		protected Resource()
		{
			
		}
		
		public Resource(string name)
		{
			Id = Guid.NewGuid();
			Name = name;
		}

		public void AddItem(ResourceItem item)
		{
			// in here we accept doubled elements in list.
			_items.Add(item);
		}

		public void UpdateItems(IEnumerable<ResourceItem> items)
		{
			// I decided just to simply replace whole collection.
			
			_items.Clear();
			_items.AddRange(items);
		}
		

		public void ChangeResourceName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				Name = name;
			}
			else
			{
				throw new ArgumentException("Invalid resource name");	
			}
			
		}
	}
}