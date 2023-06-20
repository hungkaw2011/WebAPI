using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.DTO;
using WebApp.API.Repositories;

namespace WebApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly ITokenReporsitory tokenReporsitory;

		public AuthController(UserManager<IdentityUser> userManager,ITokenReporsitory tokenReporsitory)
		{
			this.userManager = userManager;
			this.tokenReporsitory = tokenReporsitory;
		}
		[HttpPost]
		[Route("Register")]
		//POST: post/api/Register
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
		{
			var identityUser = new IdentityUser
			{
				UserName = registerRequestDto.UserName,
				Email = registerRequestDto.UserName,
			};
			var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.PassWord);
			if (identityResult.Succeeded)
			{
				if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
				{
					await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
					return Ok("Đăng ký thành công! Xin hãy đăng nhập.");
				}
			}
			return BadRequest("Đã xảy ra lỗi");
		}
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
			if (user == null)
			{
				return BadRequest("Tài khoản hoặc mật khẩu không chính xác!!");
			}
			else
			{
				var checkPaswoldResult = await userManager.CheckPasswordAsync(user, loginRequestDto.PassWord);
				if (checkPaswoldResult)
				{
					// Get Roles from this user 
					var roles = await userManager.GetRolesAsync(user);
					if (roles != null && roles.Any())
					{
						var jwtToken = tokenReporsitory.CreateJWToken(user, roles.ToList());
						var response = new LoginResponseDto
						{
							jwtToken = jwtToken,
						};
						return Ok(response);
					}
					// Create Token 
				}
			}
			return BadRequest("Đã xảy ra lỗi");
		}
	}
}
