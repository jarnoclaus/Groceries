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
            return Ok(await _context.ShoppingListItems.ToListAsync());
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
