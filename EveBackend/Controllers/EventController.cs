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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }
    }
}
