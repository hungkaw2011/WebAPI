using WebApp.API.Models.Domain;

namespace WebApp.API.Repositories
{
	public class InMemoryRegionRepository 
	{
		public async Task<List<Region>> GetAllAsync()
		{
			return new List<Region>
			{
				new Region()
				{
					Id=Guid.NewGuid(),
					Code="ac",
					Name="ga",
					RegionImageUrl="url.img"
				}
			};
		}
	}
}
