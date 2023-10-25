using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PNTestApi.Helpers;
using PNTestApi.Hubs;
using PNTestApi.Models;
using PNTestApi.DTOs;
using PNTestApi.Data;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PNTestApi.Repositories;
using PNTestApi.Interfaces;

namespace PNTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IHubContext<LocationsHub> _hubContext;
        private readonly IQueryRepository _queryRepostiory;

        public LocationsController(IHubContext<LocationsHub> hubContext, IQueryRepository QueryRepository)
        {
            _hubContext = hubContext;
            _queryRepostiory = QueryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> ReceiveLocation([FromBody] Location request, [FromQuery] string? category)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "ll", $"{request.Latitude},{request.Longitude}" }
            };

            var fsqClient = new HttpClientFSQ();
            var response = await fsqClient.GetFSQResponseAsync(queryParameters);

            if (!response.IsSuccessStatusCode) return StatusCode((int)response.StatusCode);

            await GenerateAndSendSubscriberMessage(request.Longitude, request.Latitude, category);

            var content = await response.Content.ReadAsStringAsync();
            JsonPlacesFilter jFilter = new JsonPlacesFilter(content);
            var filteredPlaces = jFilter.PlacesWithCategory(category);

            var response_body = JsonConvert.SerializeObject(filteredPlaces);
            var request_body = JsonConvert.SerializeObject(request);

            var newQuery = new Query
            {
                RequestData = request_body,
                ResponseData = response_body,
                TimeStamp = DateTime.Now
            };

            await _queryRepostiory.AddQueryAsync(newQuery);

            return Ok(response_body);
        }

        private async Task GenerateAndSendSubscriberMessage(double longitude, double latitude, string category)
        {
            var locRequest = new LocationRequest
            {
                Location = new Location
                {
                    Longitude = longitude,
                    Latitude =  latitude
                },
                Category = category
            };

            var message = JsonConvert.SerializeObject(locRequest);
            await _hubContext.Clients.All.SendAsync("ReceiveRequestInfo", message);

        }

    }
}
