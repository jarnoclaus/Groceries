using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Groceries.API.Models
{
    public class GroceryContext : DbContext
    {
        public DbSet<CatalogueItem> CatalogueItems { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public GroceryContext(DbContextOptions<GroceryContext> options) : base(options) { }
    }
}
