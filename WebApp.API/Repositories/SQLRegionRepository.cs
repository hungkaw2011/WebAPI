using Microsoft.EntityFrameworkCore;
using WebApp.API.Data;
using WebApp.API.Models.Domain;
using WebApp.API.Models.DTO;

namespace WebApp.API.Repositories
{
	public class SQLRegionRepository : IRegionRepository
	{
		private readonly ApplicationDbContext dbContext;
        public SQLRegionRepository(ApplicationDbContext dbContext)
        {
			this.dbContext = dbContext;
        }

		public async Task<Region> CreateAsync(Region region)
		{
			await dbContext.Regions.AddAsync(region);
			await dbContext.SaveChangesAsync();
			return region;
		}

		public async Task<Region?> DeleteAsync(Guid id)
		{
			var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRegion == null)
			{
				return null;
			}

			dbContext.Regions.Remove(existingRegion);
			await dbContext.SaveChangesAsync();
			return existingRegion;
		}

		public async Task<List<Region>> GetAllAsync()
		{
			return await dbContext.Regions.ToListAsync();
		}

		public async Task<Region?> GetByIdAsync(Guid id)
		{
			return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Region?> UpdateAsync(Guid id, UpdateRegionRequestDto region)
		{
			var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRegion == null)
			{
				return null;
			}

			existingRegion.Code = region.Code;
			existingRegion.Name = region.Name;
			existingRegion.RegionImageUrl = region.RegionImageUrl;

			await dbContext.SaveChangesAsync();
			return existingRegion;
		}
	}
}
