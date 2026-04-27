using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelemedApp.API.Models;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Application.UseCases.Patients;

namespace TelemedApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController(
        IPatientService patientService,
        IMapper mapper,
        CreatePatientHandler create,
        UpdatePatientHandler update,
        DeletePatientHandler delete) : ControllerBase
    {
        private readonly IPatientService _patientService = patientService;
        private readonly IMapper _mapper = mapper;

        private readonly CreatePatientHandler _create = create;
        private readonly UpdatePatientHandler _update = update;
        private readonly DeletePatientHandler _delete = delete;

        [HttpGet]
        [Authorize(Policy = "Patients.View")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _patientService.GetAllPatientsAsync();
            return Ok(ApiResponse<IEnumerable<PatientDto>>.Ok(_mapper.Map<IEnumerable<PatientDto>>(items)));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "Patients.View")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _patientService.GetPatientByIdAsync(id);
            if (entity == null)
                return NotFound(ApiResponse<object?>.Fail("Patient not found"));

            return Ok(ApiResponse<PatientDto>.Ok(_mapper.Map<PatientDto>(entity)));
        }

        [HttpPost]
        [Authorize(Policy = "Patients.Create")]
        public async Task<IActionResult> Create([FromBody] PatientDto dto)
        {
            var created = await _create.HandleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ApiResponse<PatientDto>.Ok(created, "Patient created successfully"));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "Patients.Edit")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PatientDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ApiResponse<object?>.Fail("ID mismatch"));

            var updated = await _update.HandleAsync(dto);
            return Ok(ApiResponse<PatientDto>.Ok(updated, "Patient updated successfully"));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "Patients.Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _delete.HandleAsync(id);
            return Ok(ApiResponse<object?>.Ok(null, "Patient deleted successfully"));
        }
    }
}