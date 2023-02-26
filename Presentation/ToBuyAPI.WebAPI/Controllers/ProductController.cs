using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.Product;
using ToBuyAPI.Persistence.Services;

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
		#region Post Methods
		[HttpPost]
		public async Task<IActionResult> Add([FromForm]CreateProduct model)
		{
			var result = await _productService.AddAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Delete Methods
		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteProduct model)
		{
			var result = await _productService.DeleteAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteById([FromRoute] string id)
		{
			var result = await _productService.DeleteByIdAsync(id);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("Range")]
		public async Task<IActionResult> DeleteRange(List<DeleteProduct> models)
		{
			var result = await _productService.DeleteRangeAsync(models);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Put Methods
		[HttpPut]
		public async Task<IActionResult> Update([FromForm]UpdateProduct model)
		{
			var result = await _productService.UpdateAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Get Methods
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _productService.GetAllAsync();
			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _productService.GetByIdAsync(id);
			return Ok(result);
		}
		#endregion
	}
}
