using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToBuyApı.Domain.Entities.Identity;
using ToBuyAPI.Application.Abstractions.JWT;
using ToBuyAPI.Application.DTOs.JWT;

namespace ToBuyAPI.Infrastructure.Services.JWT
{
	public class TokenHandler : ITokenHandler
	{
		private readonly IConfiguration _configuration;

		public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
		{
			_configuration = configuration;
		}

		public Token CreateAccessToken(AppUser appUser,string userRole,int minute)
		{
			Token token = new();
			//Symmetric Security key created.
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

			//Credentials creeated.
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			//Claims created
			var claims = new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.Name,appUser.FullName),
				new Claim("userRole",userRole)
			};

			//Token settings are set.
			token.Expiration = DateTime.UtcNow.AddMinutes(minute);
			JwtSecurityToken securityToken = new(
				audience: _configuration["Token:Audience"],
				issuer: _configuration["Token:Issuer"],
				expires: token.Expiration,
				signingCredentials: signingCredentials,
				claims:claims
				);

			//Token created.
			JwtSecurityTokenHandler tokenHandler = new();
			token.AccessToken = tokenHandler.WriteToken(securityToken);
			return token;
		}
	}
}
