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
		/// <summary>
		/// Yeni kategori ekleme işlemi @@@Admin
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize(policy:"Admin")]
		public async Task<IActionResult> Add(CreateCategory model)
		{
			var result = await _categoryService.AddAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		/// <summary>
		/// Birden fazla yeni kategori ekleme işlemi @@@Admin
		/// </summary>
		/// <param name="models"></param>
		/// <returns></returns>
		[HttpPost("Range")]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> AddRange(List<CreateCategory> models)
		{
			var result = await _categoryService.AddRangeAsync(models);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Delete Methods
		/// <summary>
		/// DeleteCategory nesnesi kullanılarak kategori silme işlemi @@@Admin
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpDelete]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> Delete(DeleteCategory model)
		{
			var result = await _categoryService.DeleteAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		/// <summary>
		/// Kategori Id üzerinden kategori silme işlemi @@@Admin
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> DeleteById([FromRoute]string id)
		{
			var result = await _categoryService.DeleteByIdAsync(id);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		/// <summary>
		/// Toplu olarak kategori silme işlemi @@@Admin
		/// </summary>
		/// <param name="models"></param>
		/// <returns></returns>
		[HttpDelete("Range")]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> DeleteRange(List<DeleteCategory> models)
		{
			var result = await _categoryService.DeleteRangeAsync(models);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Put Methods
		/// <summary>
		/// Var olan bir kategoriyi güncelleme işlemi @@@Admin
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> Update(UpdateCategory model)
		{
			var result = await _categoryService.UpdateAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Get Methods
		/// <summary>
		/// Tum kategorileri çağırma işlemi @@@Admin/User
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _categoryService.GetAllAsync();
			return Ok(result);
		}
		/// <summary>
		/// Kategori Id üzerinden tek bir kategori çağırma işlemi@@@Admin/User
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _categoryService.GetByIdAsync(id);
			return Ok(result);
		}
		#endregion
	}
}
