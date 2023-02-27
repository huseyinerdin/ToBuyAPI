using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.Category;

namespace ToBuyAPI.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes ="General")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		#region Post Methods
		[HttpPost]
		[Authorize(policy:"Admin")]
		public async Task<IActionResult> Add(CreateCategory model)
		{
			var result = await _categoryService.AddAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPost("Range")]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> AddRange(List<CreateCategory> models)
		{
			var result = await _categoryService.AddRangeAsync(models);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Delete Methods
		[HttpDelete]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> Delete(DeleteCategory model)
		{
			var result = await _categoryService.DeleteAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id}")]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> DeleteById([FromRoute]string id)
		{
			var result = await _categoryService.DeleteByIdAsync(id);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("Range")]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> DeleteRange(List<DeleteCategory> models)
		{
			var result = await _categoryService.DeleteRangeAsync(models);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Put Methods
		[HttpPut]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> Update(UpdateCategory model)
		{
			var result = await _categoryService.UpdateAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Get Methods
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _categoryService.GetAllAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _categoryService.GetByIdAsync(id);
			return Ok(result);
		}
		#endregion
	}
}
