using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.Product;

namespace ToBuyAPI.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromForm]CreateProduct model)
		{
			var result = await _productService.AddAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteProduct model)
		{
			var result = await _productService.DeleteAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
	}
}
