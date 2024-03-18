using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveStateController(IRepository<SaveState> repository) : BaseController
    {
        private readonly IRepository<SaveState> _saveStateRepository = repository;

        // GET: api/SaveState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaveState>>> GetSaveStates()
        {
            var result = await _saveStateRepository.All();
            return result;
        }

        // GET: api/SaveState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaveState?>> GetSaveState(Guid id)
        {
            var saveState = await _saveStateRepository.GetByID(id);

            if (saveState == null)
            {
                return NotFound();
            }

            return Ok(saveState);
        }

        // PUT: api/SaveState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaveState(Guid id, JsonContent stateUpdateData)
        {
            var target = await _saveStateRepository.GetByID(id);

            if (target.Value is null)
            {
                return BadRequest();
            }

            try
            {
                target.Value.Data = JsonSerializer.Serialize(stateUpdateData);
                await _saveStateRepository.Update(target.Value);
                await _saveStateRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!SaveStateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new DbUpdateConcurrencyException($"Could not save changes: {ex.Message}");
                }
            }

            return NoContent();
        }

        // POST: api/SaveState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaveState>> PostSaveState(JsonContent stateData)
        {
            string dataToStore = JsonSerializer.Serialize(stateData);
            SaveState newSaveState = new() { Data = dataToStore };

            try
            {
                await _saveStateRepository.Add(newSaveState);
                await _saveStateRepository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Could not save to database: {ex.Message}");
            }

            return CreatedAtAction("GetSaveState", new { id = newSaveState.Id }, newSaveState);
        }

        // DELETE: api/SaveState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaveState(Guid id)
        {
            var saveState = await _saveStateRepository.GetByID(id);

            if (saveState.Value == null)
            {
                return NotFound();
            }

            try
            {
                await _saveStateRepository.Remove(saveState.Value.Id);
                await _saveStateRepository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Could not delete saveState: {ex.Message}");
            }

            return NoContent();
        }

        private bool SaveStateExists(Guid id)
        {
            var exists = _saveStateRepository.Any(saveState => saveState.Id == id);
            return exists.Result;
        }
    }
}
