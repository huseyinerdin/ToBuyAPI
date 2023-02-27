using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToBuyApı.Domain.Entities.Identity;
using ToBuyAPI.Application.Abstractions.JWT;
using ToBuyAPI.Application.DTOs.AppUser;
using ToBuyAPI.Application.DTOs.JWT;
using ToBuyAPI.Persistence.Services.ResultService;

namespace ToBuyAPI.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ITokenHandler _tokenHandler;

		public UserController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_mapper = mapper;
			_signInManager = signInManager;
			_tokenHandler = tokenHandler;
			_roleManager = roleManager;
		}
		[HttpPost("[action]")]
		public async Task<IActionResult> Register(CreateAppUser model)
		{
			Result result = new();
			AppUser appUser = _mapper.Map<AppUser>(model);
			appUser.Id = Guid.NewGuid().ToString();
			IdentityResult identityResult = await _userManager.CreateAsync(appUser,model.Password);
			var userRole = _roleManager.FindByNameAsync("User").Result;
			await _userManager.AddToRoleAsync(appUser, userRole.Name);
			result.IsSuccess = identityResult.Succeeded; 
			if (result.IsSuccess)
			{
				result.Message = "Kullanıcı eklme işlemi başarılı.";
				return Ok(result);
			}
			else
			{
				foreach (var error in identityResult.Errors)
				{
					result.Message += $"{error.Code} - {error.Description}\n";
				}
				return BadRequest(result);
			}
		}
		[HttpPost("[action]")]
		public async Task<IActionResult> Login(LoginAppUser model)
		{
			DataResult<Token> result = new();
			AppUser appUser = await _userManager.FindByNameAsync(model.UserName);
			if (appUser == null)
			{
				result.IsSuccess=false;
				result.Message = "Kullanıcı adı veya şifre hatalı.";
				return BadRequest(result);
			}
			Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password,true);
			result.IsSuccess = signInResult.Succeeded;
			if (result.IsSuccess)
			{
				var role =  _userManager.GetRolesAsync(appUser).Result.First();
				result.Result = _tokenHandler.CreateAccessToken(appUser,role,30);
				result.Message = "Giriş işlemi başarılı.";
				return Ok(result);
			}
			else
			{
				result.Message = "Kullanıcı adı veya şifre hatalı.";
				return BadRequest(result);
			}
		}
	}
}
