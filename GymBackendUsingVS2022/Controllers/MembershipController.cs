using GymBackendUsingVS2022.Data;
using GymBackendUsingVS2022.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackendUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly DataContext _context;

        public MembershipController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //public IActionResult Get() { }
        public async Task<ActionResult<List<Membership>>> GetAllMembership()
        {
            var membership = await _context.Memberships.Include(m => m.MembershipType)
                                                       .Include(m => m.Member)
                                                       .ToListAsync();

            return Ok(membership);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Membership>>> GetMembership(int id)
        {
            var membership = await _context.Memberships.Include(m => m.MembershipType)
                                                       .Include(m => m.Member)
                                                       .FirstOrDefaultAsync(m => m.MembershipId == id);
            if (membership is null)
                return NotFound("Membership not found");

            return Ok(membership);
        }



        [HttpPost]
        public async Task<ActionResult<List<Membership>>> AddMembership(Membership membership)
        {
            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMembership), new { id = membership.MembershipId }, membership);
        }


        // PUT: api/Memberships/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemberships(int id, Membership membership)
        {
            if (id != membership.MembershipId)
            {
                return BadRequest();
            }

            _context.Entry(membership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipExists(id))
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

        private bool MembershipExists(int id)
        {
            return _context.Memberships.Any(e => e.MembershipId == id);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Membership>>> DeleteMembership(int id)
        {
            var dbMembership = await _context.Memberships.FindAsync(id);
            if (dbMembership is null)
                return NotFound("Membership not found");
            _context.Memberships.Remove(dbMembership);

            await _context.SaveChangesAsync();

            return Ok(await _context.Memberships.ToListAsync());
        }
    }
}
