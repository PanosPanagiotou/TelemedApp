using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelemedApp.API.Models;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Application.UseCases.MedicalRecords;

namespace TelemedApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController(
        IMedicalRecordService recordService,
        IMapper mapper,
        CreateMedicalRecordHandler create,
        UpdateMedicalRecordHandler update,
        DeleteMedicalRecordHandler delete) : ControllerBase
    {
        private readonly IMedicalRecordService _recordService = recordService;
        private readonly IMapper _mapper = mapper;

        private readonly CreateMedicalRecordHandler _create = create;
        private readonly UpdateMedicalRecordHandler _update = update;
        private readonly DeleteMedicalRecordHandler _delete = delete;

        [HttpGet("patient/{patientId:guid}")]
        [Authorize(Policy = "MedicalRecords.View")]
        public async Task<IActionResult> GetByPatient(Guid patientId)
        {
            var items = await _recordService.GetRecordsByPatientIdAsync(patientId);
            return Ok(ApiResponse<IEnumerable<MedicalRecordDto>>.Ok(_mapper.Map<IEnumerable<MedicalRecordDto>>(items)));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "MedicalRecords.View")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _recordService.GetRecordByIdAsync(id);
            if (entity == null)
                return NotFound(ApiResponse<object?>.Fail("Medical record not found"));

            return Ok(ApiResponse<MedicalRecordDto>.Ok(_mapper.Map<MedicalRecordDto>(entity)));
        }

        [HttpPost]
        [Authorize(Policy = "MedicalRecords.Create")]
        public async Task<IActionResult> Create([FromBody] MedicalRecordDto dto)
        {
            var created = await _create.HandleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ApiResponse<MedicalRecordDto>.Ok(created, "Medical record created successfully"));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "MedicalRecords.Edit")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicalRecordDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ApiResponse<object?>.Fail("ID mismatch"));

            var updated = await _update.HandleAsync(dto);
            return Ok(ApiResponse<MedicalRecordDto>.Ok(updated, "Medical record updated successfully"));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "MedicalRecords.Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _delete.HandleAsync(id);
            return Ok(ApiResponse<object?>.Ok(null, "Medical record deleted successfully"));
        }
    }
}