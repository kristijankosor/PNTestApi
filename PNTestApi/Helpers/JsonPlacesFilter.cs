using PNTestApi.DTOs;
using System;
using System.Numerics;
using System.Text.Json;
namespace PNTestApi.Helpers
{
    public class JsonPlacesFilter
    {
        private readonly string _jsonData;
        private List<Place> _places;

        public JsonPlacesFilter(string jsonData)
        {
            _jsonData = jsonData;
            try
            {
                _places = JsonSerializer.Deserialize<JsonResponse>(_jsonData).Results;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
            }
        }

        public List<Place> PlacesWithCategory(string? category)//ovo se moze poopcit
        {
            if (category == null) return _places;

            var filteredObjects = _places.Where(o => o.Categories.Any(k => k.Name == category)).ToList();
            return filteredObjects;
        }
    }
}
