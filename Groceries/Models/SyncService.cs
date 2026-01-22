using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groceries.Models
{
    public class SyncService
    {
        public static async Task PullFromCloudAsync()
        {
            var cloudCatalogue = await CloudService.GetCatalogueListAsync();
            var cloudShopping = await CloudService.GetShoppingListAsync();

            GroceryData.Catalogue.Clear();
            foreach (var item in cloudCatalogue)
                GroceryData.Catalogue.Add(item);

            Merge(GroceryData.ShoppingList, cloudShopping);

            await FileService.SaveCatalogueList();
            await FileService.SaveShoppingList();
        }

        public static async Task PushToCloudAsync()
        {
            await CloudService.SaveCatalogueListAsync(GroceryData.Catalogue);
            await CloudService.SaveShoppingListAsync(GroceryData.ShoppingList);
        }

        private static void Merge(ObservableCollection<GroceryItem> local, List<GroceryItem> cloud)
        {
            foreach(var cloudItem in cloud)
            {
                if (!local.Any(localItem => string.Equals(localItem.Name, cloudItem.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    local.Add(cloudItem);
                }
            }
        }
    }
}
