using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.UI.Models;
using WebApp.UI.Models.DTO;

namespace WebApp.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory httpClientFactory;

		public HomeController(ILogger<HomeController> logger,IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			this.httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			List<RegionDto> regions = new List<RegionDto>();
			try
			{
				//Get All regions from Web API
				var client = httpClientFactory.CreateClient();

				var httpResponseMessage = await client.GetAsync("https://localhost:7004/api/regions");
				httpResponseMessage.EnsureSuccessStatusCode();

#pragma warning disable CS8604 // Possible null reference argument.
				regions.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
			}
			catch (Exception)
			{
				// Log the exception
				throw;
			}
			return View(regions);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}