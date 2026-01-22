using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Groceries.Models
{
    public class CloudService
    {
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://grocery-api-749d.onrender.com/")
        };

        public static async Task SaveCatalogueListAsync(ObservableCollection<GroceryItem> list)
        {
            var json = JsonSerializer.Serialize(list);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/catalogue", content);
        }

        public static async Task SaveShoppingListAsync(ObservableCollection<GroceryItem> list)
        {
            var dtoList = list.Select(g => new ShoppingListItem
            {
                Name = g.Name,
                Amount = g.Amount
            }).ToList();
            var json = JsonSerializer.Serialize(dtoList);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/shoppinglist", content);
        }

        public static async Task<List<GroceryItem>> GetCatalogueListAsync()
        {
            var response = await _httpClient.GetAsync("api/catalogue");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GroceryItem>>(json) ?? new List<GroceryItem>();
        }

        public static async Task<List<GroceryItem>> GetShoppingListAsync()
        {
            var response = await _httpClient.GetAsync("api/shoppinglist");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GroceryItem>>(json) ?? new List<GroceryItem>();
        }

    }
}
