using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mille.Models;

namespace Mille.Commands
{
	public class CreateResourceCommand
	{
		[Required]
		public string Name { get; set; }
		
		public IEnumerable<ResourceItemModel> Items { get; set; }

		public CreateResourceCommand(string name, IEnumerable<ResourceItemModel> items)
		{
			Name = name;
			Items = items;
		}
	}
}