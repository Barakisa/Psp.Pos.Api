namespace Psp.Pos.Api.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StaffUserId { get; set; }
        public DateTime DateTime { get; set; }
        public string? Status { get; set; }
    }
}
