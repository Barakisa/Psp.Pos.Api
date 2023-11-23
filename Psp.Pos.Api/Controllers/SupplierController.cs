using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private static List<Supplier> suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Supplier A", Contacts = "Contact A" },
            new Supplier { Id = 2, Name = "Supplier B", Contacts = "Contact B" }
        };

        // GET: api/suppliers
        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetSuppliers()
        {
            return Ok(suppliers);
        }

        // GET: api/suppliers/{id}
        [HttpGet("{id}")]
        public ActionResult<Supplier> GetSupplier(int id)
        {
            var supplier = suppliers.Find(s => s.Id == id);

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
            newSupplier.Id = suppliers.Count + 1;
            suppliers.Add(newSupplier);

            return CreatedAtAction(nameof(GetSupplier), new { id = newSupplier.Id }, newSupplier);
        }

        // PUT: api/suppliers/{id}
        [HttpPut("{id}")]
        public ActionResult<Supplier> UpdateSupplier(int id, [FromBody] Supplier updatedSupplier)
        {
            var existingSupplier = suppliers.Find(s => s.Id == id);

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
            var supplierToRemove = suppliers.Find(s => s.Id == id);

            if (supplierToRemove == null)
            {
                return NotFound();
            }

            suppliers.Remove(supplierToRemove);

            return NoContent();
        }
    }
}
