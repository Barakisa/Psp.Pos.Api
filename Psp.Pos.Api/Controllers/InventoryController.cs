using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;
using System.Collections.Generic;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private static List<Inventory> _inventoryList = new List<Inventory>
        {
            new Inventory { Id = 1, ProductId = 201, SupplierId = 301, StockQuantity = 100 },
            new Inventory { Id = 2, ProductId = 202, SupplierId = 302, StockQuantity = 50 }
        };

        // GET: api/inventory
        [HttpGet]
        public ActionResult<PaginatableResponseObject<IEnumerable<Inventory>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var inventoryList = _inventoryList;
            var response = new PaginatableResponseObject<IEnumerable<Inventory>>();
            var itemsToSkip = (page - 1) * pageSize;
            response.Data = inventoryList.Skip(itemsToSkip).Take(pageSize).ToList();
            response.nextPage = "https://localhost:7064/api/Inventory?page=" + (page + 1) + "&pageSize=" + pageSize;
            return Ok(response);
        }

        // GET: api/inventory/{id}
        [HttpGet("{id}")]
        public ActionResult<Inventory> GetInventoryItem(int id)
        {
            var inventoryItem = _inventoryList.Find(item => item.Id == id);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            return Ok(inventoryItem);
        }

        // POST: api/inventory
        [HttpPost]
        public ActionResult<Inventory> CreateInventoryItem([FromBody] Inventory newInventoryItem)
        {
            newInventoryItem.Id = _inventoryList.Count + 1;
            _inventoryList.Add(newInventoryItem);

            return CreatedAtAction(nameof(GetInventoryItem), new { id = newInventoryItem.Id }, newInventoryItem);
        }

        // PUT: api/inventory/{id}
        [HttpPut("{id}")]
        public ActionResult<Inventory> UpdateInventoryItem(int id, [FromBody] Inventory updatedInventoryItem)
        {
            var existingInventoryItem = _inventoryList.Find(item => item.Id == id);

            if (existingInventoryItem == null)
            {
                return NotFound();
            }

            // Update the existing inventory item with the new data
            existingInventoryItem.ProductId = updatedInventoryItem.ProductId;
            existingInventoryItem.SupplierId = updatedInventoryItem.SupplierId;
            existingInventoryItem.StockQuantity = updatedInventoryItem.StockQuantity;

            return Ok(existingInventoryItem);
        }

        // DELETE: api/inventory/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteInventoryItem(int id)
        {
            var inventoryItemToRemove = _inventoryList.Find(item => item.Id == id);

            if (inventoryItemToRemove == null)
            {
                return NotFound();
            }

            _inventoryList.Remove(inventoryItemToRemove);

            return NoContent();
        }
    }
}
