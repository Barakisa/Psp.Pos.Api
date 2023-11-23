using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using Psp.Pos.Api.Models;
using System;
using System.Collections.Generic;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChequeController : ControllerBase
    {
        private static List<Cheque> cheques = new List<Cheque>
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
                PaymentType = PaymentType.PrivateCard.ToString(),
                Tax = 50,
                TotalDiscount = 100,
                Price = 1800,
                PriceWithTax = 1850,
                PriceWithTaxAndDiscount = 1750
            }
            // Add more Cheque instances as needed
        };

        // GET: api/cheque/{id}
        [HttpGet("{id}")]
        public ActionResult<Cheque> GetChequeById(int id)
        {
            var cheque = cheques.Find(c => c.Id == id);

            if (cheque == null)
            {
                return NotFound();
            }

            return Ok(cheque);
        }
        // GET: api/cheque/orderId/{orderId}
        [HttpGet("orderId/{orderId}")]
        public ActionResult<Cheque> GetChequeByOrderId(int orderId)
        {
            var cheque = cheques.Find(c => c.OrderId == orderId);

            if (cheque == null)
            {
                return NotFound();
            }

            return Ok(cheque);
        }
    }
}
