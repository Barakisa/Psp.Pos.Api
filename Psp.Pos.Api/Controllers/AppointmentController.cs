using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Psp.Pos.Api.Models;
using System.Drawing.Printing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private static readonly  List<Appointment> _appointments = new List<Appointment>
        {
            new Appointment { Id = 1, CustomerId = 101, StaffUserId = 201, DateTime = DateTime.Now.AddDays(1), Status = "Pending" },
            new Appointment { Id = 2, CustomerId = 102, StaffUserId = 202, DateTime = DateTime.Now.AddDays(2), Status = "Confirmed" }
        };

        // GET: api/Appointments
        [HttpGet]
        public ActionResult<PaginatableResponseObject<IEnumerable<Appointment>>> GetAppointments([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var appointments = _appointments;
            var response = new PaginatableResponseObject<IEnumerable<Appointment>>();
            var itemsToSkip = (page - 1) * pageSize;
            response.Data = _appointments.Skip(itemsToSkip).Take(pageSize).ToList();
            response.nextPage = "https://localhost:7064/api/Appointments?page=" + (page + 1) + "&pageSize=" + pageSize;
            return Ok(response);
        }

        // GET: api/Appointments/{id}
        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            var appointment = _appointments.Find(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // POST: api/Appointments
        [HttpPost]
        public ActionResult<Appointment> CreateAppointment([FromBody] Appointment newAppointment)
        {
            newAppointment.Id = _appointments.Count + 1;
            _appointments.Add(newAppointment);

            return CreatedAtAction(nameof(GetAppointment), new { id = newAppointment.Id }, newAppointment);
        }

        // PUT: api/Appointments/{id}
        [HttpPut("{id}")]
        public ActionResult<Appointment> UpdateAppointment(int id, [FromBody] Appointment updatedAppointment)
        {
            var existingAppointment = _appointments.Find(a => a.Id == id);

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

        // DELETE: api/Appointments/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAppointment(int id)
        {
            var appointmentToRemove = _appointments.Find(a => a.Id == id);

            if (appointmentToRemove == null)
            {
                return NotFound();
            }

            _appointments.Remove(appointmentToRemove);

            return NoContent();
        }
    }
}
