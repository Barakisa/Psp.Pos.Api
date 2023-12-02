using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Psp.Pos.Api.Models;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private static List<OrderItems> _orderItemsList = new List<OrderItems>
        {
            new OrderItems { OrderId = 101, ProductId = 201, Quantity = 2 },
            new OrderItems { OrderId = 102, ProductId = 202, Quantity = 1 }
        };

        // GET: api/suppliers
        [HttpGet]
        public ActionResult<PaginatableResponseObject<IEnumerable<OrderItems>>> GetSuppliers([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var response = new PaginatableResponseObject<IEnumerable<OrderItems>>();
            var itemsToSkip = (page - 1) * pageSize;
            response.Data = _orderItemsList.Skip(itemsToSkip).Take(pageSize).ToList();
            response.nextPage = "https://localhost:7064/api/Products?page=" + (page + 1) + "&pageSize=" + pageSize;
            return Ok(response);
        }

        // GET: api/OrderItems/{orderId}/{productId}
        [HttpGet("{orderId}/{productId}")]
        public ActionResult<OrderItems> GetOrderItems(int orderId, int productId)
        {
            var orderItems = _orderItemsList.Find(od => od.OrderId == orderId && od.ProductId == productId);

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
            _orderItemsList.Add(newOrderItems);

            return CreatedAtAction(nameof(GetOrderItems), newOrderItems);
        }

        // PUT: api/OrderItems/{orderId}/{productId}
        [HttpPut("{orderId}/{productId}")]
        public ActionResult<OrderItems> UpdateOrderItems([FromBody] OrderItems updatedOrderItems)
        {
            var existingOrderItems = _orderItemsList.Find(od => od.OrderId == updatedOrderItems.OrderId && od.ProductId == updatedOrderItems.ProductId);

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
            var orderItemsToRemove = _orderItemsList.Find(od => od.OrderId == orderId && od.ProductId == productId);

            if (orderItemsToRemove == null)
            {
                return NotFound();
            }

            _orderItemsList.Remove(orderItemsToRemove);

            return NoContent();
        }
    }
}
