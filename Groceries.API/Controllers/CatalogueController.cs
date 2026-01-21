using Groceries.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Groceries.API.Controllers
{
    [Route("api/catalogue")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {
        private readonly GroceryContext _context;
        public CatalogueController(GroceryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogue()
        {
            return Ok(await _context.CatalogueItems.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SaveListToDatabase([FromBody] List<CatalogueItem> items)
        {
            _context.CatalogueItems.RemoveRange(_context.CatalogueItems);
            _context.CatalogueItems.AddRange(items);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
