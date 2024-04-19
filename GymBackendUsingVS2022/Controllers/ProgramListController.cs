using GymBackendUsingVS2022.Data;
using GymBackendUsingVS2022.Entities;
using GymBackendUsingVS2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackendUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramListController : ControllerBase
    {
        private readonly DataContext _context;

        public ProgramListController(DataContext context)
        {
            _context = context;
        }

        
        //public IActionResult Get() { }
        // GET: api/programlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramList>>> GetProgramLists()
        {
            return await _context.ProgramLists.Include(p => p.WorkoutPlans)
                                              .Include(p => p.MembershipTypes)
                                              .ToListAsync();
        }

        // GET: api/programlists/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramList>> GetProgramList(int id)
        {
            var programList = await _context.ProgramLists.FindAsync(id);

            if (programList == null)
            {
                return NotFound();
            }

            return programList;
        }

        // POST: api/programlists
        [HttpPost]
        public async Task<ActionResult<ProgramList>> CreateProgramList(ProgramList programList)
        {
            _context.ProgramLists.Add(programList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProgramList), new { id = programList.ProgramListId }, programList);
        }

        // PUT: api/programlists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgramList(int id, ProgramList programList)
        {
            if (id != programList.ProgramListId)
            {
                return BadRequest();
            }

            _context.Entry(programList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramListExists(id))
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

        // DELETE: api/programlists/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramList(int id)
        {
            var programList = await _context.ProgramLists.FindAsync(id);
            if (programList == null)
            {
                return NotFound();
            }

            _context.ProgramLists.Remove(programList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramListExists(int id)
        {
            return _context.ProgramLists.Any(e => e.ProgramListId == id);
        }
    }


}

