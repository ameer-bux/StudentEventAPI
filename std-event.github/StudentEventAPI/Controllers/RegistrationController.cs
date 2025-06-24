using Microsoft.AspNetCore.Mvc;
using StudentEventAPI.DTOs;
using StudentEventAPI.Services;

namespace StudentEventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(RegistrationDTO dto)
        {
            var message = await _registrationService.RegisterStudentAsync(dto);
            if (message.Contains("not found") || message.Contains("already"))
                return BadRequest(new { message });

            return Ok(new { message });
        }
    }
}
