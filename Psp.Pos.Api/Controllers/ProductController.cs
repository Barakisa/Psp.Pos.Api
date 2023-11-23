using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;
using System.Collections.Generic;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Category = "Category A", Price = 500 }, // assuming the price is in cents
            new Product { Id = 2, Name = "Product B", Category = "Category B", Price = 800 }
        };

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.Find(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product newProduct)
        {
            newProduct.Id = products.Count + 1;
            products.Add(newProduct);

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var existingProduct = products.Find(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the existing product with the new data
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Category = updatedProduct.Category;
            existingProduct.Price = updatedProduct.Price;

            return Ok(existingProduct);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var productToRemove = products.Find(p => p.Id == id);

            if (productToRemove == null)
            {
                return NotFound();
            }

            products.Remove(productToRemove);

            return NoContent();
        }
    }
}
