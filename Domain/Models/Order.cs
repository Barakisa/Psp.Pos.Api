namespace Psp.Pos.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime DateTime { get; set; }
    }

}
