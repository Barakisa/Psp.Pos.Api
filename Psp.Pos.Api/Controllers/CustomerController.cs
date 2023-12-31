﻿using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static List<Customer> _customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "John Doe", Email = "john.doe@pspsps.lt", LoyaltyPoints = 100, LoyaltyLevel = "Silver", FeedBack = new[] { "Great service!" } },
            new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@pspsps.lt", LoyaltyPoints = 200, LoyaltyLevel = "Gold", FeedBack = new[] { "Excellent experience!" } }
        };

        // GET: api/customers
        [HttpGet]
        public ActionResult<PaginatableResponseObject<IEnumerable<Customer>>> GetCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var customers = _customers;
            var response = new PaginatableResponseObject<IEnumerable<Customer>>();
            var itemsToSkip = (page - 1) * pageSize;
            response.Data = customers.Skip(itemsToSkip).Take(pageSize).ToList();
            response.nextPage = "https://localhost:7064/api/Customers?page=" + (page + 1) + "&pageSize=" + pageSize;
            return Ok(response);
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _customers.Find(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer newCustomer)
        {
            newCustomer.Id = _customers.Count + 1;
            _customers.Add(newCustomer);

            return CreatedAtAction(nameof(GetCustomer), new { id = newCustomer.Id }, newCustomer);
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public ActionResult<Customer> UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            var existingCustomer = _customers.Find(c => c.Id == id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update the existing customer with the new data
            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.LoyaltyPoints = updatedCustomer.LoyaltyPoints;
            existingCustomer.LoyaltyLevel = updatedCustomer.LoyaltyLevel;
            existingCustomer.FeedBack = updatedCustomer.FeedBack;

            return Ok(existingCustomer);
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var customerToRemove = _customers.Find(c => c.Id == id);

            if (customerToRemove == null)
            {
                return NotFound();
            }

            _customers.Remove(customerToRemove);

            return NoContent();
        }
    }
}
