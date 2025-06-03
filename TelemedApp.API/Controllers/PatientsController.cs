using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;

namespace TelemedApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController(IPatientService patientService) : ControllerBase
    {
        private readonly IPatientService _patientService = patientService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientDto patient)
        {
            await _patientService.CreatePatientAsync(patient);
            return Ok();
        }

        // Add GetById, Update, Delete as needed
    }
}
