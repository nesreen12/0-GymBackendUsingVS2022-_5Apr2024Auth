using GymBackendUsingVS2022.Data;
using GymBackendUsingVS2022.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackendUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly DataContext _context;

        public MemberController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //public IActionResult Get() { }
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembers()
        {
            var member = await _context.Members.Include(m => m.User).Include(m => m.Membership)./*Include(m => m.WorkoutPlans).*/ToListAsync();

            return Ok(member);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.Include(m => m.User)
                                   .Include(m => m.Membership)
                                   .Include(m => m.WorkoutPlans)
                                   .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member is null)
                return NotFound("Member not found");

            return Ok(member);
        }


        [HttpPost]
        public async Task<ActionResult<Member>> AddMember(Member member)
        {
            // Check if the User with the given UserId has RoleId 1
            var user = await _context.Users.FindAsync(member.UserId);
            if (user == null || user.RoleId != "1")
            {
                return BadRequest("Only users with RoleId 3 can be assigned as Members.");
            }
            // Check if an Instructor with the given UserId already exists
            bool memberExists = await _context.Members.AnyAsync(m => m.UserId == member.UserId);
            if (memberExists)
            {
                return BadRequest("A member with the given UserId already exists.");
            }

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest("The ID does not match the member's ID.");
            }

            // Check if the User with the given UserId exists and has RoleId 1
            var user = await _context.Users.FindAsync(member.UserId);
            if (user == null || user.RoleId != "1")
            {
                return BadRequest("Only users with RoleId 3 can be assigned as members.");
            }

            // Check if an Member with the given UserId already exists and is not the current member
            bool anotherMemberExists = await _context.Members
                .AnyAsync(m => m.UserId == member.UserId && m.MemberId != id);
            if (anotherMemberExists)
            {
                return BadRequest("Another member with the given UserId already exists.");
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(member);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
