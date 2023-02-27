using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.Product;
using ToBuyAPI.Application.DTOs.ToBuyList;
using ToBuyAPI.Persistence.Services;

namespace ToBuyAPI.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToBuyListController : ControllerBase
	{
		private readonly IToBuyListService _toBuyListService;

		public ToBuyListController(IToBuyListService toBuyListService)
		{
			_toBuyListService = toBuyListService;
		}
		#region Post Methods
		[HttpPost]
		public async Task<IActionResult> Add(CreateToBuyList model)
		{
			var result = await _toBuyListService.AddAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Delete Methods
		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteToBuyList model)
		{
			var result = await _toBuyListService.DeleteAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteById([FromRoute] string id)
		{
			var result = await _toBuyListService.DeleteByIdAsync(id);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("Range")]
		public async Task<IActionResult> DeleteRange(List<DeleteToBuyList> models)
		{
			var result = await _toBuyListService.DeleteRangeAsync(models);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Put Methods
		[HttpPut]
		public async Task<IActionResult> Update(UpdateToBuyList model)
		{
			var result = await _toBuyListService.UpdateAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

		#region Get Methods
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _toBuyListService.GetAllAsync();
			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _toBuyListService.GetByIdAsync(id);
			return Ok(result);
		}
		#endregion
	}
}
