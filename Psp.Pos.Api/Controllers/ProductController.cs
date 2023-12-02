using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;
using System.Collections.Generic;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Product A", Category = "Category A", Price = 500 }, // assuming the price is in cents
            new Product { Id = 2, Name = "Product B", Category = "Category B", Price = 800 }
        };

        // GET: api/products
        [HttpGet]
        public ActionResult<PaginatableResponseObject<IEnumerable<Product>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var response = new PaginatableResponseObject<IEnumerable<Product>>();
            var itemsToSkip = (page - 1) * pageSize;
            response.Data = _products.Skip(itemsToSkip).Take(pageSize).ToList();
            response.nextPage = "https://localhost:7064/api/Products?page=" + (page + 1) + "&pageSize=" + pageSize;
            return Ok(response);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var products = _products;
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
            var products = _products;
            newProduct.Id = products.Count + 1;
            products.Add(newProduct);

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var products = _products;
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
            var products = _products;
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
