using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;
using Psp.Pos.Api.Services;
using static NuGet.Packaging.PackagingConstants;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static List<Order> _orders = new List<Order>
        {
            new Order { Id = 1, AppointmentId = 101, DateTime = DateTime.Now.AddDays(1) },
            new Order { Id = 2, AppointmentId = 102, DateTime = DateTime.Now.AddDays(2) }
        };

        private static List<Cheque> _cheques = new List<Cheque>
        {
            new Cheque
            {
                Id = 1,
                OrderId = 101,
                Time = (new DateTime(2023, 3, 15, 15, 15, 15, DateTimeKind.Utc).Ticks - 621355968000000000) / 10000, //time since unix epoch - January 1st, 1970, 00:00:00.000 UTC
                ItemsWithPrices = new List<OrderItemsWithPrices>
                {
                    new OrderItemsWithPrices { Name = 201, Quantity = 2, Price = 500 },
                    new OrderItemsWithPrices { Name = 202, Quantity = 1, Price = 800 }
                },
                Appointment = new Appointment { Id = 1, CustomerId = 101, StaffUserId = 201, DateTime = DateTime.Now.AddDays(1), Status = "Pending" },
                PaymentType = PaymentType.PrivateCard.ToString(),
                Tax = 50,
                TotalDiscount = 100,
                Price = 1800,
                PriceWithTax = 1850,
                PriceWithTaxAndDiscount = 1750
            },
            new Cheque
            {
                Id = 2,
                OrderId = 102,
                Time = (new DateTime(2023, 3, 15, 15, 15, 15, DateTimeKind.Utc).Ticks - 621355968000000000) / 10000, //time since unix epoch - January 1st, 1970, 00:00:00 UTC
                ItemsWithPrices = new List<OrderItemsWithPrices>
                {
                    new OrderItemsWithPrices { Name = 201, Quantity = 2, Price = 500 },
                    new OrderItemsWithPrices { Name = 202, Quantity = 1, Price = 800 }
                },
                Appointment = new Appointment { Id = 2, CustomerId = 102, StaffUserId = 202, DateTime = DateTime.Now.AddDays(2), Status = "Confirmed" },
                PaymentType = PaymentType.PrivateCard.ToString(),
                Tax = 50,
                TotalDiscount = 100,
                Price = 1800,
                PriceWithTax = 1850,
                PriceWithTaxAndDiscount = 1750
            }
            // Add more Cheque instances as needed
        };


        // GET: api/orders
        [HttpGet]
        public ActionResult<PaginatableResponseObject<IEnumerable<Cheque>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 1, [FromQuery] bool fullInfo = false)
        {
            var orders = _orders;
            var response = new PaginatableResponseObject<IEnumerable<Cheque>>();
            var itemsToSkip = (page - 1) * pageSize;
            response.Data = _cheques.Skip(itemsToSkip).Take(pageSize).ToList();
            response.nextPage = "https://localhost:7064/api/Orders?page=" + (page + 1) + "&pageSize=" + pageSize;
            return Ok(response);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public ActionResult<Cheque> GetOrder(int id, [FromQuery] bool fullInfo = false)
        {
            var order = _cheques.Find(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public ActionResult<Cheque> CreateOrder([FromBody] Cheque newOrder, [FromQuery] bool fullInfo = false)
        {
            //newOrder.Id = _cheques.Count + 1;
            //_cheques.Add(newOrder);

            //sita returninti
            OrderService.process(newOrder).ToResponse();

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public ActionResult<Cheque> UpdateOrder(int id, [FromBody] Cheque updatedOrder, [FromQuery] bool fullInfo = false)
        {
            var existingOrder = _cheques.Find(o => o.Id == id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update the existing order with the new data
            existingOrder.Appointment.Id = updatedOrder.Appointment.Id;
            existingOrder.Appointment.DateTime = updatedOrder.Appointment.DateTime;

            return Ok(existingOrder);
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var orderToRemove = _orders.Find(o => o.Id == id);

            if (orderToRemove == null)
            {
                return NotFound();
            }

            _orders.Remove(orderToRemove);

            return NoContent();
        }
    }
}
