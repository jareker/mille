using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mille.Models;

namespace Mille.Commands
{
	public class UpdateResourceCommand
	{
		[Required]
		public Guid Id { get; set; }
		
		[Required]
		public string Name { get; set; }
		
		public IEnumerable<ResourceItemModel> Items { get; set; }
	}
}