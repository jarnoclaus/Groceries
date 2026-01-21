using System.ComponentModel.DataAnnotations;

namespace Groceries.API.Models
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 999)]
        public int Amount { get; set; } = 1;

    }
}
