using GymBackendUsingVS2022.Data;
using GymBackendUsingVS2022.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackendUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkoutPlanController(DataContext context)
        {
            _context = context;
        }
        // GET: api/workoutplans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutPlan>>> GetWorkoutPlans()
        {
            return await _context.WorkoutPlans.ToListAsync();
        }

        // GET: api/workoutplans/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutPlan>> GetWorkoutPlan(int id)
        {
            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);

            if (workoutPlan == null)
            {
                return NotFound();
            }

            return workoutPlan;
        }

        // POST: api/workoutplans
        [HttpPost]
        public async Task<ActionResult<WorkoutPlan>> CreateWorkoutPlan(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlans.Add(workoutPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkoutPlan), new { id = workoutPlan.WorkoutPlanId }, workoutPlan);
        }

        // PUT: api/workoutplans/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkoutPlan(int id, WorkoutPlan workoutPlan)
        {
            if (id != workoutPlan.WorkoutPlanId)
            {
                return BadRequest();
            }

            _context.Entry(workoutPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutPlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/workoutplans/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutPlan(int id)
        {
            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            _context.WorkoutPlans.Remove(workoutPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkoutPlanExists(int id)
        {
            return _context.WorkoutPlans.Any(e => e.WorkoutPlanId == id);
        }
    }
}
