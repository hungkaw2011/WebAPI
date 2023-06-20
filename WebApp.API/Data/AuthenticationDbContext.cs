using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.API.Data
{
	public class AuthenticationDbContext : IdentityDbContext
	{
		public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "d12046fe-7556-4dfa-8ab8-7271f3fdf54b";
			var writerRoleId = "5dccfbd8-fd24-4eec-8bd7-189f2733b88a";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id= readerRoleId,
					ConcurrencyStamp= readerRoleId,
					Name="Reader",
					NormalizedName="Reader".ToUpper()
				},
				new IdentityRole
				{
					Id= writerRoleId,
					ConcurrencyStamp= writerRoleId,
					Name="Writer",
					NormalizedName="Writer".ToUpper()
				}
			};
			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
