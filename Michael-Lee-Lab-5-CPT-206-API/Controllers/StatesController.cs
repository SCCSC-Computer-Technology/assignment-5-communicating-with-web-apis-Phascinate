using Michael_Lee_Lab_5_CPT_206.Data.Context;
using Microsoft.AspNetCore.Mvc;
using StatesDatabaseClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace Michael_Lee_Lab_5_CPT_206.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly StatesContext _context;

        public StatesController(StatesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates()
        {
            return await _context.States.ToListAsync();
        }

        // Gets an invidual state using its ID
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
                return NotFound();
            return state;
        }

        // Adds a new state to the DB
        [HttpPost]
        public async Task<ActionResult<State>> AddState(State state)
        {
            _context.States.Add(state);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetState), new { id = state.StateID }, state);
        }

        // Updates a state that already exists in the DB
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState(int id, State state)
        {
            if (id != state.StateID)
                return BadRequest();

            _context.Entry(state).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a state from the DB
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
                return NotFound();

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Search by several different metrics
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<State>>> SearchStates(
        string? name,
        string? capital,
        string? flower,
        string? bird,
        string? colors,
        string? largestCity,
        int? minPopulation,
        int? maxPopulation,
        decimal? minIncome,
        decimal? maxIncome)
        {
            var query = _context.States.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(s => s.StateName.Contains(name));

            if (!string.IsNullOrWhiteSpace(capital))
                query = query.Where(s => s.StateCapital.Contains(capital));

            if (!string.IsNullOrWhiteSpace(flower))
                query = query.Where(s => s.StateFlower.Contains(flower));

            if (!string.IsNullOrWhiteSpace(bird))
                query = query.Where(s => s.StateBird.Contains(bird));

            if (!string.IsNullOrWhiteSpace(colors))
                query = query.Where(s => s.Colors.Contains(colors));

            if (!string.IsNullOrWhiteSpace(largestCity))
                query = query.Where(s => s.LargestCities.Contains(largestCity));

            if (minPopulation.HasValue)
                query = query.Where(s => s.Population >= minPopulation);

            if (maxPopulation.HasValue)
                query = query.Where(s => s.Population <= maxPopulation);

            if (minIncome.HasValue)
                query = query.Where(s => s.MedianIncome >= minIncome);

            if (maxIncome.HasValue)
                query = query.Where(s => s.MedianIncome <= maxIncome);

            return await query.ToListAsync();
        }

    }

}
