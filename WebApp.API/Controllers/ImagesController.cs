using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.Domain;
using WebApp.API.Models.DTO;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
		{
			this.imageRepository = imageRepository;
		}
		[HttpPost]
		[Route("Upload")]
		public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequest)
		{
			ValidateFileUpLoad(imageUploadRequest);
			if (ModelState.IsValid)
			{
				//Convert DTO to Domain model
				var imgDomain = new Image
				{
					File = imageUploadRequest.File,
					FileExtension = Path.GetExtension(imageUploadRequest.File.FileName),
					FileSizeInBytes = imageUploadRequest.File.Length,
					FileName = imageUploadRequest.FileName,
					FileDescription = imageUploadRequest.FileDescription,
				};
				// User repository to upload image
				await imageRepository.Upload(imgDomain);

				return Ok(imgDomain);
			}
			return BadRequest(ModelState);
		}
		private void ValidateFileUpLoad(ImageUploadRequestDto imageUploadRequestDto)
		{
			var allowedExtensions = new String[] { ".jpg", ".jepg", ".png" };
			if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
			{
				ModelState.AddModelError("File", "Dạng File không được hỗ trợ!!");
			}
			if (imageUploadRequestDto.File.Length > (1024 * 1024))
			{
				ModelState.AddModelError("File", "Kích thước Filekhông được vượt quá 10 Mb!!");
			}
		}
	}
}
