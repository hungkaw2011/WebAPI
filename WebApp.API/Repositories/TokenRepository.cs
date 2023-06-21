using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApp.API.Repositories
{
	public class TokenRepository : ITokenReporsitory
	{
		private readonly IConfiguration configuration;

		public TokenRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public string CreateJWToken(IdentityUser user, List<string> roles)
		{
			//Một claim chứa thông tin về người dùng và thường được biểu diễn dưới dạng cặp tên-giá trị 
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Email, user.Email!));
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				configuration["Jwt:Issuer"],
				configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddDays(7),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
