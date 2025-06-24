using StudentEventAPI.DTOs;

namespace StudentEventAPI.Services
{
    public interface IEventService
    {
        Task<List<EventResponseDTO>> GetAllEventsAsync();
        Task<EventResponseDTO> CreateEventAsync(EventDTO dto);
        Task<bool> UpdateEventAsync(int id, EventDTO dto);
        Task<bool> DeleteEventAsync(int id);
        Task<List<EventResponseDTO>> SearchEventsAsync(string query);
        Task<List<EventResponseDTO>> FilterEventsAsync(string sortBy);
    }
}
