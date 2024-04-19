using GymBackendUsingVS2022.Data;
using GymBackendUsingVS2022.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackendUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public MembershipTypeController(DataContext context)
        {
            _context = context;
        }

        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipType>>> GetMembershipTypes()
        {
            return await _context.MembershipTypes.Include(m => m.ProgramLists)
                                                  .Include(m => m.Memberships)
                                                 .ToListAsync();
        }
        
        // GET: api/membershiptypes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipType>> GetMembershipType(int id)
        {
            var membershipType = await _context.MembershipTypes.FindAsync(id);

            if (membershipType == null)
            {
                return NotFound();
            }

            return membershipType;
        }

        // POST: api/membershiptypes
        [HttpPost]
        public async Task<ActionResult<MembershipType>> CreateMembershipType(MembershipType membershipType)
        {
            _context.MembershipTypes.Add(membershipType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMembershipType), new { id = membershipType.MembershipTypeId }, membershipType);
        }

        // PUT: api/membershiptypes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMembershipType(int id, MembershipType membershipType)
        {
            if (id != membershipType.MembershipTypeId)
            {
                return BadRequest();
            }

            _context.Entry(membershipType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipTypeExists(id))
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

        // DELETE: api/membershiptypes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembershipType(int id)
        {
            var membershipType = await _context.MembershipTypes.FindAsync(id);
            if (membershipType == null)
            {
                return NotFound();
            }

            _context.MembershipTypes.Remove(membershipType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MembershipTypeExists(int id)
        {
            return _context.MembershipTypes.Any(e => e.MembershipTypeId == id);
        }
    }
}
