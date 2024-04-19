using GymBackendUsingVS2022.Data;
using GymBackendUsingVS2022.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackendUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly DataContext _context;

        public InstructorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //public IActionResult Get() { }
        public async Task<ActionResult<IEnumerable<Instructor>>> GetAllInstructor()
        {
            var instructor = await _context.Instructors.Include(i => i.User).Include(i => i.WorkoutPlans).ToListAsync();

            return Ok(instructor);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetInstructor(int id)
        {
            var instructor = await _context.Instructors.Include(i => i.User)
                                   .Include(i => i.WorkoutPlans)
                                   .FirstOrDefaultAsync(i => i.InstructorId == id);
            if (instructor is null)
                return NotFound("Instructor not found");

            return Ok(instructor);
        }


        [HttpPost]
        public async Task<ActionResult<Instructor>> AddInstructor(Instructor instructor)
        {
            // Check if the User with the given UserId has RoleId 2
            var user = await _context.Users.FindAsync(instructor.UserId);
            if (user == null || user.RoleId != "2")
            {
                return BadRequest("Only users with RoleId 2 can be assigned as instructors.");
            }
            // Check if an Instructor with the given UserId already exists
            bool instructorExists = await _context.Instructors.AnyAsync(i => i.UserId == instructor.UserId);
            if (instructorExists)
            {
                return BadRequest("An instructor with the given UserId already exists.");
            }

            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstructor", new { id = instructor.InstructorId }, instructor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstructor(int id, Instructor instructor)
        {
            if (id != instructor.InstructorId)
            {
                return BadRequest("The ID does not match the instructor's ID.");
            }

            // Check if the User with the given UserId exists and has RoleId 2
            var user = await _context.Users.FindAsync(instructor.UserId);
            if (user == null || user.RoleId != "2")
            {
                return BadRequest("Only users with RoleId 2 can be assigned as instructors.");
            }

            // Check if an Instructor with the given UserId already exists and is not the current instructor
            bool anotherInstructorExists = await _context.Instructors
                .AnyAsync(i => i.UserId == instructor.UserId && i.InstructorId != id);
            if (anotherInstructorExists)
            {
                return BadRequest("Another instructor with the given UserId already exists.");
            }

            _context.Entry(instructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(instructor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.InstructorId == id);
        }


    }
}
