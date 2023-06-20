using Microsoft.AspNetCore.Identity;

namespace WebApp.API.Repositories
{
	public interface ITokenReporsitory
	{
		string CreateJWToken(IdentityUser user, List<string> roles);
	}
}
