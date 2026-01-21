using Groceries.API.DTOs;
using Groceries.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Groceries.API.Controllers
{
    [Route("api/shoppinglist")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly GroceryContext _context;

        public ShoppingListController(GroceryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingList()
        {
            var items = await _context.ShoppingListItems.Select(i => new ShoppingListItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Amount = i.Amount,
                isPickedUp = false
            }).ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> SaveListToDatabase([FromBody] List<ShoppingListItem> items)
        {
            _context.ShoppingListItems.RemoveRange(_context.ShoppingListItems);
            _context.ShoppingListItems.AddRange(items);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
