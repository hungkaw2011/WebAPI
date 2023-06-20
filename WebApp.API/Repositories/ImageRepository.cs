using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApp.API.Data;
using WebApp.API.Models.Domain;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace WebApp.API.Repositories.IRepository
{
	public class ImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly ApplicationDbContext dbContext;

		public ImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,ApplicationDbContext dbContext)
		{
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.dbContext = dbContext;
		}

		public async Task<Image> Upload(Image image)
		{
			var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
				$"{image.FileName}{image.FileExtension}");
			// C:\Users\hungq\source\repos\WebAPI\WebApp.API\Images\{image.FileName}.{image.FileExtension}

			// Upload New Image to Local Path
			using var stream = new FileStream(localFilePath, FileMode.Create);
			await image.File.CopyToAsync(stream);

			// https://localhost:1234/images/image.jpg
			var urlFilePath = $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}" +
				$"{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
			image.FilePath = urlFilePath;

			// Add Image to the Images table
			await dbContext.Images.AddAsync(image);
			await dbContext.SaveChangesAsync();

			return image;
		}
	}
}
