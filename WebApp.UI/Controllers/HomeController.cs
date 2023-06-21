using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text;
using WebApp.UI.Models;
using WebApp.UI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> regions = new List<RegionDto>();
            try
            {
                var client = httpClientFactory.CreateClient();

                // Lấy mã thông báo JWT từ nguồn (ví dụ: từ cookie, từ lưu trữ, ...)
                string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb" +
                    "2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJodW5na2FAZ21haWwu" +
                    "Y29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb" +
                    "2xlIjoiV3JpdGVyIiwiZXhwIjoxNjg3OTYxMDQyLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDA0LyIsImF" +
                    "1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMDQvIn0.aOmQrbqaVaas9l2cJw2-kDyxYeztV7kWb_-3NMWJSq0";

                // Đặt giá trị mã thông báo JWT vào tiêu đề yêu cầu
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var httpResponseMessage = await client.GetAsync("https://hungka.azurewebsites.net/api/regions");
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

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Privacy(AddRegionViewModel addRegionViewModel)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://hungka.azurewebsites.net/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://hungka.azurewebsites.net/api/regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://hungka.azurewebsites.net/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (respose is not null)
            {
                return RedirectToAction("Edit", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://hungka.azurewebsites.net/api/regions/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                // Console
            }

            return View("Edit");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}