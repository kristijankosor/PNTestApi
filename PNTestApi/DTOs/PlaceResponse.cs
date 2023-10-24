using System.Text.Json.Serialization;
using System.Collections.Generic;


namespace PNTestApi.DTOs
{
   
    public class JsonResponse
    {
        [JsonPropertyName("results")]
        public List<Place> Results { get; set; }
    }
    public class Place
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("categories")]
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}
