using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;
using System.Transactions;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private static List<Supplier> _suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier A", Contacts = "Contact A" },
            new Supplier { Id = 2, Name = "Supplier B", Contacts = "Contact B" }
        };

        // GET: api/suppliers
        [HttpGet]
        public ActionResult<PaginatableResponseObject<IEnumerable<Supplier>>> GetSuppliers([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var response = new PaginatableResponseObject<IEnumerable<Supplier>>();
            var itemsToSkip = (page - 1) * pageSize;
            response.Data = _suppliers.Skip(itemsToSkip).Take(pageSize).ToList();
            response.nextPage = "https://localhost:7064/api/Products?page=" + (page + 1) + "&pageSize=" + pageSize;
            return Ok(response);
        }

        // GET: api/suppliers/{id}
        [HttpGet("{id}")]
        public ActionResult<Supplier> GetSupplier(int id)
        {
            var supplier = _suppliers.Find(s => s.Id == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // POST: api/suppliers
        [HttpPost]
        public ActionResult<Supplier> CreateSupplier([FromBody] Supplier newSupplier)
        {
            newSupplier.Id = _suppliers.Count + 1;
            _suppliers.Add(newSupplier);

            return CreatedAtAction(nameof(GetSupplier), new { id = newSupplier.Id }, newSupplier);
        }

        // PUT: api/suppliers/{id}
        [HttpPut("{id}")]
        public ActionResult<Supplier> UpdateSupplier(int id, [FromBody] Supplier updatedSupplier)
        {
            var existingSupplier = _suppliers.Find(s => s.Id == id);

            if (existingSupplier == null)
            {
                return NotFound();
            }

            // Update the existing supplier with the new data
            existingSupplier.Name = updatedSupplier.Name;
            existingSupplier.Contacts = updatedSupplier.Contacts;

            return Ok(existingSupplier);
        }

        // DELETE: api/suppliers/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteSupplier(int id)
        {
            var supplierToRemove = _suppliers.Find(s => s.Id == id);

            if (supplierToRemove == null)
            {
                return NotFound();
            }

            _suppliers.Remove(supplierToRemove);

            return NoContent();
        }
    }
}
