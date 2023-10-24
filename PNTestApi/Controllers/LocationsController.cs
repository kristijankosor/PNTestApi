using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PNTestApi.Helpers;
using PNTestApi.Models;
using System.Text.Json;

namespace PNTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ReceiveLocation([FromBody] Location request, [FromQuery] string? category)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "ll", $"{request.Latitude},{request.Longitude}" }
            };

            var fsqClient = new HttpClientFSQ(queryParameters);
            await fsqClient.setResponseAsync();
            var response = fsqClient.fsqResponse;

            if (!response.IsSuccessStatusCode) return StatusCode((int)response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            JsonPlacesFilter jFilter = new JsonPlacesFilter(content);
            var response_body = jFilter.PlacesByCategoryJson(category);

            return Ok(response_body);
        }
    }
}
