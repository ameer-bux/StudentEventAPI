using Microsoft.AspNetCore.Mvc;
using StudentEventAPI.DTOs;
using StudentEventAPI.Services;

namespace StudentEventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: /api/event
        [HttpGet]
        public async Task<ActionResult<List<EventResponseDTO>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // POST: /api/event
        [HttpPost]
        public async Task<ActionResult<EventResponseDTO>> CreateEvent(EventDTO dto)
        {
            var createdEvent = await _eventService.CreateEventAsync(dto);
            return CreatedAtAction(nameof(GetAllEvents), new { id = createdEvent.Id }, createdEvent);
        }

        // PUT: /api/event/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent(int id, EventDTO dto)
        {
            var result = await _eventService.UpdateEventAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: /api/event/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // GET: /api/event/search?query=xyz
        [HttpGet("search")]
        public async Task<ActionResult<List<EventResponseDTO>>> Search(string query)
        {
            var events = await _eventService.SearchEventsAsync(query);
            return Ok(events);
        }

        // GET: /api/event/filter?sort=date
        [HttpGet("filter")]
        public async Task<ActionResult<List<EventResponseDTO>>> Filter(string sort)
        {
            var events = await _eventService.FilterEventsAsync(sort);
            return Ok(events);
        }
    }
}
