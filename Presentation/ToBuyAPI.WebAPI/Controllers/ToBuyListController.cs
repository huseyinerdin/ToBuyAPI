using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.ToBuyList;

namespace ToBuyAPI.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "General")]
	public class ToBuyListController : ControllerBase
	{
		private readonly IToBuyListService _toBuyListService;

		public ToBuyListController(IToBuyListService toBuyListService)
		{
			_toBuyListService = toBuyListService;
		}
		#region Post Methods
		/// <summary>
		/// Yeni liste oluşturma işlemi @@@Admin/User
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Add(CreateToBuyList model)
		{
			var result = await _toBuyListService.AddAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Delete Methods
		/// <summary>
		/// DeleteToBuyList nesnesi kullanılarak liste silme işlemi @@@Admin/User
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteToBuyList model)
		{
			var result = await _toBuyListService.DeleteAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		/// <summary>
		/// Liste id üzerinden liste silme işlemi @@@Admin/User
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteById([FromRoute] string id)
		{
			var result = await _toBuyListService.DeleteByIdAsync(id);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		/// <summary>
		/// Toplu olarak liste silme işlemi @@@Admin/User
		/// </summary>
		/// <param name="models"></param>
		/// <returns></returns>
		[HttpDelete("Range")]
		public async Task<IActionResult> DeleteRange(List<DeleteToBuyList> models)
		{
			var result = await _toBuyListService.DeleteRangeAsync(models);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Put Methods
		/// <summary>
		/// Var olan bir listeyi güncelleme işlemi @@@Admin/User
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> Update(UpdateToBuyList model)
		{
			var result = await _toBuyListService.UpdateAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Get Methods
		/// <summary>
		/// Tüm listeleri çağırma işlemi @@@Admin
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(policy: "Admin")]
		public async Task<IActionResult> GetAll()
		{
			var result = await _toBuyListService.GetAllAsync();
			return Ok(result);
		}
		/// <summary>
		/// Kullanıcı Id üzerinden kullanıcıya ait listeleri çağırma işlemi @@@Admin/User
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("[action]/{id}")]
		public async Task<IActionResult> GetAllByUserId(string id)
		{
			var result = await _toBuyListService.GetAllAsync(id);
			return Ok(result);
		}
		/// <summary>
		/// Liste Id üzerinden tek bir listeyi çağırma işlemi
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _toBuyListService.GetByIdAsync(id);
			return Ok(result);
		}
		#endregion
	}
}
