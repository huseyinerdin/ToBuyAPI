using Microsoft.AspNetCore.Http;

namespace ToBuyAPI.Application.DTOs.Product
{
	public class CreateProduct
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public IFormFileCollection ProductImageFiles { get; set; }
		public List<string> CategoryIds { get; set; }
	}
}
