using GymBackendUsingVS2022.Data;
using GymBackendUsingVS2022.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackendUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly DataContext _context;

        public ClassController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //public IActionResult Get() { }
        public async Task<ActionResult<List<Class>>> GetAllClass()
        {
            var clas = await _context.classes.Include(c =>c.WorkoutPlan).ToListAsync();

            return Ok(clas);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Class>>> GetClass(int id)
        {
            var clas = await _context.classes.Include(c => c.WorkoutPlan).FirstOrDefaultAsync(c => c.ClassId == id);
            if (clas is null)
                return NotFound("Class not found");

            return Ok(clas);
        }



        [HttpPost]
        public async Task<ActionResult<List<Class>>> AddClass(Class clas)
        {
            _context.classes.Add(clas);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClass),new {id = clas.ClassId }, clas);
        }


        // PUT: api/classes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class clas)
        {
            if (id != clas.ClassId)
            {
                return BadRequest();
            }

            _context.Entry(clas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        private bool ClassExists(int id)
        {
            return _context.classes.Any(e => e.ClassId == id);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Class>>> DeleteClass(int id)
        {
            var dbClass = await _context.classes.FindAsync(id);
            if (dbClass is null)
                return NotFound("Class not found");
            _context.classes.Remove(dbClass);

            await _context.SaveChangesAsync();

            return Ok(await _context.classes.ToListAsync());
        }




    }










    //[HttpPut("{id}")]
    //public async Task<ActionResult<List<Class>>> UpdateClass(Class updatedClass)
    //{
    //    var dbClass = await _context.classes.FindAsync(updatedClass.ClassId);
    //    if (dbClass is null)
    //        return NotFound("Class not found");
    //    dbClass.Day = updatedClass.Day;
    //    dbClass.StartTime = updatedClass.StartTime;
    //    dbClass.EndTime = updatedClass.EndTime;
    //    dbClass.Location = updatedClass.Location;
    //    dbClass.WorkoutPlanId = updatedClass.WorkoutPlanId;

    //    await _context.SaveChangesAsync();

    //    return Ok(await _context.classes.FindAsync());
    //}
}
