using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBuyAPI.Application.DTOs.ToBuyList
{
	public class CreateToBuyList
	{
		public string Name { get; set; }
		public List<string> CategoryIds { get; set; }
		public string CustomerId { get; set; }
	}
}
