using Microsoft.EntityFrameworkCore;
using WebApp.API.Models.Domain;

namespace WebApp.API.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{

		}
		public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walk> Walks { get; set; }

	}
}
