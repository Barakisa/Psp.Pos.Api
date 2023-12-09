namespace Psp.Pos.Domain
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int StockQuantity { get; set; }
    }
}
