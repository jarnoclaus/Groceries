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
        public static async Task SyncAynsc()
        {

            await CloudService.SaveCatalogueListAsync(GroceryData.Catalogue);
            await CloudService.SaveShoppingListAsync(GroceryData.ShoppingList);

            var cloudCatalogue = await CloudService.GetCatalogueListAsync();
            var cloudShopping = await CloudService.GetShoppingListAsync();

            Merge(GroceryData.Catalogue, cloudCatalogue);
            Merge(GroceryData.ShoppingList, cloudShopping);

            await FileService.SaveCatalogueList();
            await FileService.SaveShoppingList();
        }

        private static void Merge(ObservableCollection<GroceryItem> local, List<GroceryItem> cloud)
        {
            local.Clear();
            foreach(var item in cloud)
            {
                local.Add(item);
            }
        }
    }
}
