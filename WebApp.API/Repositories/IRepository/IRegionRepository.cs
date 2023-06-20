using WebApp.API.Models.Domain;
using WebApp.API.Models.DTO;

namespace WebApp.API.Repositories
{
	public interface IRegionRepository
	{
		Task<List<Region>> GetAllAsync();

		Task<Region?> GetByIdAsync(Guid id);

		Task<Region> CreateAsync(Region region);

		Task<Region?> UpdateAsync(Guid id, UpdateRegionRequestDto region);

		Task<Region?> DeleteAsync(Guid id);

	}
}
