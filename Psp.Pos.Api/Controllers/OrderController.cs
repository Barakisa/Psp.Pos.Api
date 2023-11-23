using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static List<Order> orders = new List<Order>
        {
            new Order { Id = 1, AppointmentId = 101, DateTime = DateTime.Now.AddDays(1) },
            new Order { Id = 2, AppointmentId = 102, DateTime = DateTime.Now.AddDays(2) }
        };

        // GET: api/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = orders.Find(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order newOrder)
        {
            newOrder.Id = orders.Count + 1;
            orders.Add(newOrder);

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public ActionResult<Order> UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            var existingOrder = orders.Find(o => o.Id == id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update the existing order with the new data
            existingOrder.AppointmentId = updatedOrder.AppointmentId;
            existingOrder.DateTime = updatedOrder.DateTime;

            return Ok(existingOrder);
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var orderToRemove = orders.Find(o => o.Id == id);

            if (orderToRemove == null)
            {
                return NotFound();
            }

            orders.Remove(orderToRemove);

            return NoContent();
        }
    }
}
