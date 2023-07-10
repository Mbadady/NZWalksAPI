using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private string _regionUrl;

        public RegionsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            //_regionUrl = configuration.GetValue<string>("ServiceUrls:RegionAPI");
            _regionUrl = configuration["ServiceUrls:RegionAPI"];

        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();

            try
            {

                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync(_regionUrl + "/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();

                 response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_regionUrl + "/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json")
            };

           var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();


            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();
                                                //OR
            //var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            //var response = JsonSerializer.Deserialize<RegionDTO>(stringResponse);

            if(response != null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {

                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync(_regionUrl + "/api/regions/" + id);

                httpResponseMessage.EnsureSuccessStatusCode();

                // Below can be in place of line 97 and 104
                //var response = await client.GetFromJsonAsync<RegionDTO>(_regionUrl + "/api/regions/" + id);

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

                if(response != null)
                {
                    return View(response);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(_regionUrl + "/api/regions/" + request.Id),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if(response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {

                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync(_regionUrl + "/api/regions/" + id);

                httpResponseMessage.EnsureSuccessStatusCode();

                // Below can be in place of line 97 and 104
                //var response = await client.GetFromJsonAsync<RegionDTO>(_regionUrl + "/api/regions/" + id);

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

                if (response != null)
                {
                    return View(response);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(_regionUrl + "/api/regions/" + request.Id);

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }

            return View("Delete");
        }
    }
}

