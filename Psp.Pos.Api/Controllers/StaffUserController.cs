using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffUsersController : ControllerBase
    {
        private static List<StaffUser> staffUsers = new List<StaffUser>
        {
            new StaffUser { Id = 1, Name = "John Staff", Email = "john.staff@pspsps.lt", Role = "Waitstaff", Password = "password1" },
            new StaffUser { Id = 2, Name = "Jane Manager", Email = "jane.manager@pspsps.lt", Role = "Manager", Password = "password2" }
        };

        // GET: api/staffusers
        [HttpGet]
        public ActionResult<IEnumerable<StaffUser>> GetStaffUsers()
        {
            return Ok(staffUsers);
        }

        // GET: api/staffusers/{id}
        [HttpGet("{id}")]
        public ActionResult<StaffUser> GetStaffUser(int id)
        {
            var staffUser = staffUsers.Find(s => s.Id == id);

            if (staffUser == null)
            {
                return NotFound();
            }

            return Ok(staffUser);
        }

        // POST: api/staffusers
        [HttpPost]
        public ActionResult<StaffUser> CreateStaffUser([FromBody] StaffUser newStaffUser)
        {
            newStaffUser.Id = staffUsers.Count + 1;
            staffUsers.Add(newStaffUser);

            return CreatedAtAction(nameof(GetStaffUser), new { id = newStaffUser.Id }, newStaffUser);
        }

        // PUT: api/staffusers/{id}
        [HttpPut("{id}")]
        public ActionResult<StaffUser> UpdateStaffUser(int id, [FromBody] StaffUser updatedStaffUser)
        {
            var existingStaffUser = staffUsers.Find(s => s.Id == id);

            if (existingStaffUser == null)
            {
                return NotFound();
            }

            // Update the existing staff user with the new data
            existingStaffUser.Name = updatedStaffUser.Name;
            existingStaffUser.Email = updatedStaffUser.Email;
            existingStaffUser.Role = updatedStaffUser.Role;
            existingStaffUser.Password = updatedStaffUser.Password;

            return Ok(existingStaffUser);
        }

        // DELETE: api/staffusers/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteStaffUser(int id)
        {
            var staffUserToRemove = staffUsers.Find(s => s.Id == id);

            if (staffUserToRemove == null)
            {
                return NotFound();
            }

            staffUsers.Remove(staffUserToRemove);

            return NoContent();
        }
    }
}
