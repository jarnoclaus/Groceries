using Groceries.API.DTOs;
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
            var items = await _context.CatalogueItems.Select(i => new CatalogueItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Priority = i.Priority
            }).ToListAsync();
            return Ok(items);
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
