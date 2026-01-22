using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groceries.Models
{
    public class ShoppingListItem
    {
        public string Name { get; set; }
        public int Amount { get; set; } = 1;
    }
}
