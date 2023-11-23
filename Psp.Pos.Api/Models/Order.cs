namespace Psp.Pos.Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
