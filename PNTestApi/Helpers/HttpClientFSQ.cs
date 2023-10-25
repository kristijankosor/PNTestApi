using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace PNTestApi.Helpers
{
    public class HttpClientFSQ
    {
        string _foursquareApiKey = "fsq35VtgasevJgZvwqcohoAtpmgK74ivDxpoKWJ6TpT2430="; // ovo stavi u config

        public async Task<HttpResponseMessage> GetFSQResponseAsync(Dictionary<String, String> queryParams)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _foursquareApiKey);
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                var uriBuilder = new UriBuilder("https://api.foursquare.com/v3/places/search");
                uriBuilder.Query = new FormUrlEncodedContent(queryParams).ReadAsStringAsync().Result;

                var fsqResponse = await httpClient.GetAsync(uriBuilder.Uri);

                return fsqResponse;
            }
        }
    }
}
