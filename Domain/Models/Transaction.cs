namespace Psp.Pos.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int StaffUserId{ get; set; }
        public string PaymentType { get; set; }
        public int DiscountApplied { get; set; }
        public int Tax { get; set; }
        public int Tip { get; set; }
        public int TotalDiscount { get; set; }
    }
}
