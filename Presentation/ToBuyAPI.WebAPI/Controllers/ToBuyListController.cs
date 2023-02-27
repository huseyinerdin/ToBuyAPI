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
		public async Task<IActionResult> Add([FromForm]CreateToBuyList model)
		{
			var result = await _toBuyListService.AddAsync(model);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}
		#endregion

	}
}
