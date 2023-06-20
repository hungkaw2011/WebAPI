using WebApp.API.Models.Domain;

namespace WebApp.API.Repositories.IRepository
{
	public interface IImageRepository
	{
		Task<Image> Upload(Image image);
	}
}
