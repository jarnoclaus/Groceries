using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace Groceries.Models
{
    class FileService
    {
        private const string fileNameCatalogue = "catalogue.json";
        private const string fileNameShopping = "shopping.json";

        // Catalogue list file methods
        public static async Task SaveCatalogueList()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, fileNameCatalogue);

            var list = GroceryData.Catalogue.ToList();

            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(filePath, json);
        }
        public static async Task LoadCatalogueList()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, fileNameCatalogue);

            GroceryData.Catalogue ??= new ObservableCollection<GroceryItem>();

            if(File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                var list = JsonSerializer.Deserialize<List<GroceryItem>>(json) ?? new List<GroceryItem>();
                GroceryData.Catalogue.Clear();
                foreach(var item in list)
                    GroceryData.Catalogue.Add(item);
            }
        }

        // Shopping list file methods
        public static async Task SaveShoppingList()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, fileNameShopping);

            var list = GroceryData.ShoppingList.ToList();

            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(filePath, json);
        }
        public static async Task LoadShoppingList()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, fileNameShopping);

            GroceryData.ShoppingList ??= new ObservableCollection<GroceryItem>();

            if(File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                var list = JsonSerializer.Deserialize<List<GroceryItem>>(json) ?? new List<GroceryItem>();
                GroceryData.ShoppingList.Clear();
                foreach (var item in list)
                    GroceryData.ShoppingList.Add(item);
            }
        }
    }
}
