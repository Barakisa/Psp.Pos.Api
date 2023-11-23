using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private static List<Appointment> appointments = new List<Appointment>
        {
            new Appointment { Id = 1, CustomerId = 101, StaffUserId = 201, DateTime = DateTime.Now.AddDays(1), Status = "Pending" },
            new Appointment { Id = 2, CustomerId = 102, StaffUserId = 202, DateTime = DateTime.Now.AddDays(2), Status = "Confirmed" }
        };

        // GET: api/appointments
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return Ok(appointments);
        }

        // GET: api/appointments/{id}
        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            var appointment = appointments.Find(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // POST: api/appointments
        [HttpPost]
        public ActionResult<Appointment> CreateAppointment([FromBody] Appointment newAppointment)
        {
            newAppointment.Id = appointments.Count + 1;
            appointments.Add(newAppointment);

            return CreatedAtAction(nameof(GetAppointment), new { id = newAppointment.Id }, newAppointment);
        }

        // PUT: api/appointments/{id}
        [HttpPut("{id}")]
        public ActionResult<Appointment> UpdateAppointment(int id, [FromBody] Appointment updatedAppointment)
        {
            var existingAppointment = appointments.Find(a => a.Id == id);

            if (existingAppointment == null)
            {
                return NotFound();
            }

            // Update the existing appointment with the new data
            existingAppointment.CustomerId = updatedAppointment.CustomerId;
            existingAppointment.StaffUserId = updatedAppointment.StaffUserId;
            existingAppointment.DateTime = updatedAppointment.DateTime;
            existingAppointment.Status = updatedAppointment.Status;

            return Ok(existingAppointment);
        }

        // DELETE: api/appointments/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAppointment(int id)
        {
            var appointmentToRemove = appointments.Find(a => a.Id == id);

            if (appointmentToRemove == null)
            {
                return NotFound();
            }

            appointments.Remove(appointmentToRemove);

            return NoContent();
        }
    }
}
