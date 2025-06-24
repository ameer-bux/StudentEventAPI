using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Data;
using StudentEventAPI.DTOs;
using StudentEventAPI.Models;

namespace StudentEventAPI.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventResponseDTO>> GetAllEventsAsync()
        {
            return await _context.Events
                .Select(e => new EventResponseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Venue = e.Venue,
                    Date = e.Date
                }).ToListAsync();
        }

        public async Task<EventResponseDTO> CreateEventAsync(EventDTO dto)
        {
            var newEvent = new Event
            {
                Name = dto.Name,
                Venue = dto.Venue,
                Date = dto.Date
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return new EventResponseDTO
            {
                Id = newEvent.Id,
                Name = newEvent.Name,
                Venue = newEvent.Venue,
                Date = newEvent.Date
            };
        }

        public async Task<bool> UpdateEventAsync(int id, EventDTO dto)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null) return false;

            existingEvent.Name = dto.Name;
            existingEvent.Venue = dto.Venue;
            existingEvent.Date = dto.Date;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<EventResponseDTO>> SearchEventsAsync(string query)
        {
            return await _context.Events
                .Where(e => e.Name.Contains(query) || e.Venue.Contains(query))
                .Select(e => new EventResponseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Venue = e.Venue,
                    Date = e.Date
                }).ToListAsync();
        }

        public async Task<List<EventResponseDTO>> FilterEventsAsync(string sortBy)
        {
            IQueryable<Event> query = _context.Events;

            if (sortBy == "date")
                query = query.OrderBy(e => e.Date);
            else if (sortBy == "venue")
                query = query.OrderBy(e => e.Venue);

            return await query
                .Select(e => new EventResponseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Venue = e.Venue,
                    Date = e.Date
                }).ToListAsync();
        }
    }
}
