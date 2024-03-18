using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Repositories;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveStateController : ControllerBase
    {
        private readonly GameDbContext _context;

        public SaveStateController(GameDbContext context)
        {
            _context = context;
        }

        // GET: api/SaveState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaveState>>> GetSaveStates()
        {
            return await _context.SaveStates.ToListAsync();
        }

        // GET: api/SaveState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaveState>> GetSaveState(Guid id)
        {
            var saveState = await _context.SaveStates.FindAsync(id);

            if (saveState == null)
            {
                return NotFound();
            }

            return saveState;
        }

        // PUT: api/SaveState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaveState(Guid id, SaveState saveState)
        {
            if (id != saveState.Id)
            {
                return BadRequest();
            }

            _context.Entry(saveState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaveStateExists(id))
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

        // POST: api/SaveState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaveState>> PostSaveState(SaveState saveState)
        {
            _context.SaveStates.Add(saveState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaveState", new { id = saveState.Id }, saveState);
        }

        // DELETE: api/SaveState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaveState(Guid id)
        {
            var saveState = await _context.SaveStates.FindAsync(id);
            if (saveState == null)
            {
                return NotFound();
            }

            _context.SaveStates.Remove(saveState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaveStateExists(Guid id)
        {
            return _context.SaveStates.Any(e => e.Id == id);
        }
    }
}
