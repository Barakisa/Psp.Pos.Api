using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;
using System.Collections.Generic;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private static List<Inventory> inventoryList = new List<Inventory>
        {
            new Inventory { Id = 1, ProductId = 201, SupplierId = 301, StockQuantity = 100 },
            new Inventory { Id = 2, ProductId = 202, SupplierId = 302, StockQuantity = 50 }
        };

        // GET: api/inventory
        [HttpGet]
        public ActionResult<IEnumerable<Inventory>> GetInventory()
        {
            return Ok(inventoryList);
        }

        // GET: api/inventory/{id}
        [HttpGet("{id}")]
        public ActionResult<Inventory> GetInventoryItem(int id)
        {
            var inventoryItem = inventoryList.Find(item => item.Id == id);

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
            newInventoryItem.Id = inventoryList.Count + 1;
            inventoryList.Add(newInventoryItem);

            return CreatedAtAction(nameof(GetInventoryItem), new { id = newInventoryItem.Id }, newInventoryItem);
        }

        // PUT: api/inventory/{id}
        [HttpPut("{id}")]
        public ActionResult<Inventory> UpdateInventoryItem(int id, [FromBody] Inventory updatedInventoryItem)
        {
            var existingInventoryItem = inventoryList.Find(item => item.Id == id);

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
            var inventoryItemToRemove = inventoryList.Find(item => item.Id == id);

            if (inventoryItemToRemove == null)
            {
                return NotFound();
            }

            inventoryList.Remove(inventoryItemToRemove);

            return NoContent();
        }
    }
}
