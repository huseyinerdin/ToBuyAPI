using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;

namespace ToBuyAPI.Application.DTOs.Product
{
	public class UpdateProduct
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool isImagesUpdated { get; set; }
		public IFormFileCollection ProductImageFiles { get; set; }
		public List<string> CategoryIds { get; set; }
	}
}
