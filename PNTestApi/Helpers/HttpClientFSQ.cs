using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace PNTestApi.Helpers
{
    public class HttpClientFSQ
    {
        string _foursquareApiKey = "fsq35VtgasevJgZvwqcohoAtpmgK74ivDxpoKWJ6TpT2430="; // ovo stavi u config
        public HttpResponseMessage fsqResponse { get; set; }
        Dictionary<string, string> _queryParams {  get; set; }

        public HttpClientFSQ(Dictionary<string, string> queryParams) 
        {
            _queryParams = queryParams;
        }

        public async Task setResponseAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _foursquareApiKey);
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                var uriBuilder = new UriBuilder("https://api.foursquare.com/v3/places/search");
                uriBuilder.Query = new FormUrlEncodedContent(_queryParams).ReadAsStringAsync().Result;

                fsqResponse = await httpClient.GetAsync(uriBuilder.Uri);
            }
        }
    }
}
