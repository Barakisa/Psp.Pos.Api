using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Packaging.Signing;

namespace Psp.Pos.Api.Models
{
    public class Cheque
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public long Time { get; set; }
        public List<OrderItemsWithPrices> ItemsWithPrices { get; set; }
        public String PaymentType { get; set; }
        public int Tax { get; set; }
        public int TotalDiscount { get; set; }
        public int Price { get; set; }
        public int PriceWithTax { get; set; }
        public int PriceWithTaxAndDiscount { get; set; }
    }
}
