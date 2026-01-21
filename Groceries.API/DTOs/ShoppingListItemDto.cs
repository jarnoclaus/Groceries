namespace Groceries.API.DTOs
{
    public class ShoppingListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool isPickedUp { get; set; }
    }
}
