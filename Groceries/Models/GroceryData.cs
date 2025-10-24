using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groceries.Models
{
    public class GroceryData
    {
        public static ObservableCollection<GroceryItem> Catalogue { get; set; }
            /*= new ObservableCollection<GroceryItem>()
        {
                new GroceryItem() {Name = "Ketchup", Priority = 5},
                new GroceryItem() {Name = "Boter", Priority = 4},
                new GroceryItem() {Name = "Boomstammetjes", Priority = 3},
                new GroceryItem() {Name = "Appelmoes", Priority = 2},
                new GroceryItem() {Name = "Krielpatatjes", Priority = 1},
                new GroceryItem() {Name = "Hespenrolletjes", Priority = 1},
                new GroceryItem() {Name = "Groen zout", Priority = 2}
        };*/
        public static ObservableCollection<GroceryItem> ShoppingList { get; set; } = new();
    }
}
