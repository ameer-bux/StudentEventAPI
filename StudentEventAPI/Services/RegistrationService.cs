using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Data;
using StudentEventAPI.DTOs;
using StudentEventAPI.Models;

namespace StudentEventAPI.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ApplicationDbContext _context;

        public RegistrationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> RegisterStudentAsync(RegistrationDTO dto)
        {
            // Check if student and event exist
            var student = await _context.Students.FindAsync(dto.StudentId);
            var ev = await _context.Events.FindAsync(dto.EventId);

            if (student == null || ev == null)
                return "Student or Event not found.";

            // Check for duplicate registration
            var alreadyRegistered = await _context.Registrations
                .AnyAsync(r => r.StudentId == dto.StudentId && r.EventId == dto.EventId);

            if (alreadyRegistered)
                return "Student already registered for this event.";

            var registration = new Registration
            {
                StudentId = dto.StudentId,
                EventId = dto.EventId
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return "Registration successful.";
        }
    }
}
