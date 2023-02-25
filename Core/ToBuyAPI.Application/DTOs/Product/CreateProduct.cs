using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.DTOs.Category;

namespace ToBuyAPI.Application.DTOs.Product
{
	public class CreateProduct
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public IFormCollection ProductImageFiles { get; set; }
		public List<string> CategoryIds { get; set; }
	}
}
