using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EveBackend.Data;
using EveBackend.Models;

namespace EveBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EveBackendDbContext _context;

        public EventsController(EveBackendDbContext context)
        {
            _context = context;
        }

        // GET /api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents([FromQuery] DateTime? date, [FromQuery] string category)
        {
            var query = _context.Events.AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(e => e.Date.Date == date.Value.Date);
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(e => e.Description.Contains(category, StringComparison.OrdinalIgnoreCase));
            }

            return await query.ToListAsync();
        }

        // POST /api/events
        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event eveEvent)
        {
            _context.Events.Add(eveEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvents), new { id = eveEvent.Id }, eveEvent);
        }

        // PUT /api/events/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event eveEvent)
        {
            if (id != eveEvent.Id)
            {
                return BadRequest();
            }

            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                return NotFound();
            }

            existingEvent.Name = eveEvent.Name;
            existingEvent.Description = eveEvent.Description;
            existingEvent.Date = eveEvent.Date;
            existingEvent.Location = eveEvent.Location;
            existingEvent.MaxAttendees = eveEvent.MaxAttendees;

            _context.Entry(existingEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/events/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eveEvent = await _context.Events.FindAsync(id);

            if (eveEvent == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eveEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST /api/events/{id}/rsvp
        [HttpPost("{id}/rsvp")]
        public async Task<IActionResult> RSVPToEvent(int id, [FromBody] string attendeeName)
        {
            var eveEvent = await _context.Events.FindAsync(id);

            if (eveEvent == null)
            {
                return NotFound();
            }

            if (eveEvent.Attendees.Count >= eveEvent.MaxAttendees)
            {
                return BadRequest("The event has reached its maximum number of attendees.");
            }

            eveEvent.Attendees.Add(attendeeName);
            _context.Entry(eveEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(eveEvent);
        }
    }
}
