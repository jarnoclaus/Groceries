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
        public static ObservableCollection<GroceryItem> ShoppingList { get; set; } = new();
    }
}
