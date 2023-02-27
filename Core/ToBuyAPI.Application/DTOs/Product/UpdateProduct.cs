using Microsoft.AspNetCore.Http;

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
