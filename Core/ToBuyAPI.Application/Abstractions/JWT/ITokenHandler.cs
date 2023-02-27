using ToBuyApı.Domain.Entities.Identity;
using ToBuyAPI.Application.DTOs.JWT;

namespace ToBuyAPI.Application.Abstractions.JWT
{
	public interface ITokenHandler
	{
		Token CreateAccessToken(AppUser appUser,string userRole,int minute);

	}
}
