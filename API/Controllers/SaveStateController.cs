using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Repositories;
using API.Models;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveStateController(SaveStateRepository<SaveState> repository) : BaseController
    {
        private readonly SaveStateRepository<SaveState> saveStateRepository = repository;

        // GET: api/SaveState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaveState>>> GetSaveStates()
        {
            var result = await saveStateRepository.All();
            return result;
        }

        // GET: api/SaveState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaveState?>> GetSaveState(Guid id)
        {
            var saveState = await saveStateRepository.GetByID(id);

            if (saveState == null)
            {
                return NotFound();
            }

            return saveState;
        }

        // PUT: api/SaveState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaveState(Guid id, JsonContent stateUpdateData)
        {
            var target = await saveStateRepository.GetByID(id);

            if (target.Value is null)
            {
                return BadRequest();
            }

            try
            {
                target.Value.Data = JsonSerializer.Serialize(stateUpdateData);
                await saveStateRepository.Update(target.Value);
                await saveStateRepository.SaveChanges();
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
                await saveStateRepository.Add(newSaveState);
                await saveStateRepository.SaveChanges();
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
            var saveState = await saveStateRepository.GetByID(id);

            if (saveState.Value == null)
            {
                return NotFound();
            }

            try
            {
                await saveStateRepository.Remove(saveState.Value.Id);
                await saveStateRepository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Could not delete saveState: {ex.Message}");
            }

            return NoContent();
        }

        private bool SaveStateExists(Guid id)
        {
            var exists = saveStateRepository.Any(saveState => saveState.Id == id);
            return exists.Result;
        }
    }
}
