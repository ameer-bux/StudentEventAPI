using StudentEventAPI.DTOs;

namespace StudentEventAPI.Services
{
    public interface IRegistrationService
    {
        Task<string> RegisterStudentAsync(RegistrationDTO dto);
    }
}
