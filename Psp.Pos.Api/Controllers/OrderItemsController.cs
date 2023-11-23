using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Psp.Pos.Api.Models;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private static List<OrderItems> OrderItemsList = new List<OrderItems>
        {
            new OrderItems { OrderId = 101, ProductId = 201, Quantity = 2 },
            new OrderItems { OrderId = 102, ProductId = 202, Quantity = 1 }
        };

        // GET: api/OrderItems
        [HttpGet]
        public ActionResult<IEnumerable<OrderItems>> GetOrderItems()
        {
            return Ok(OrderItemsList);
        }

        // GET: api/OrderItems/{orderId}/{productId}
        [HttpGet("{orderId}/{productId}")]
        public ActionResult<OrderItems> GetOrderItems(int orderId, int productId)
        {
            var orderItems = OrderItemsList.Find(od => od.OrderId == orderId && od.ProductId == productId);

            if (orderItems == null)
            {
                return NotFound();
            }

            return Ok(orderItems);
        }

        // POST: api/OrderItems
        [HttpPost]
        public ActionResult<OrderItems> CreateOrderItems([FromBody] OrderItems newOrderItems)
        {
            OrderItemsList.Add(newOrderItems);

            return CreatedAtAction(nameof(GetOrderItems), newOrderItems);
        }

        // PUT: api/OrderItems/{orderId}/{productId}
        [HttpPut("{orderId}/{productId}")]
        public ActionResult<OrderItems> UpdateOrderItems([FromBody] OrderItems updatedOrderItems)
        {
            var existingOrderItems = OrderItemsList.Find(od => od.OrderId == updatedOrderItems.OrderId && od.ProductId == updatedOrderItems.ProductId);

            if (existingOrderItems == null)
            {
                return NotFound();
            }

            // Update the existing order Items with the new data
            existingOrderItems.OrderId = updatedOrderItems.OrderId;
            existingOrderItems.ProductId = updatedOrderItems.ProductId;
            existingOrderItems.Quantity = updatedOrderItems.Quantity;

            return Ok(existingOrderItems);
        }

        // DELETE: api/OrderItems/{orderId}/{productId}
        [HttpDelete("{orderId}/{productId}")]
        public ActionResult DeleteOrderItems(int orderId, int productId)
        {
            var orderItemsToRemove = OrderItemsList.Find(od => od.OrderId == orderId && od.ProductId == productId);

            if (orderItemsToRemove == null)
            {
                return NotFound();
            }

            OrderItemsList.Remove(orderItemsToRemove);

            return NoContent();
        }
    }
}
