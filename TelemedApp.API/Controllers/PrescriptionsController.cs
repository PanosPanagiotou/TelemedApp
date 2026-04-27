using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelemedApp.API.Models;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Application.UseCases.Prescriptions;

namespace TelemedApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController(
        IPrescriptionService prescriptionService,
        IMapper mapper,
        CreatePrescriptionHandler create,
        UpdatePrescriptionHandler update,
        DeletePrescriptionHandler delete) : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService = prescriptionService;
        private readonly IMapper _mapper = mapper;

        private readonly CreatePrescriptionHandler _create = create;
        private readonly UpdatePrescriptionHandler _update = update;
        private readonly DeletePrescriptionHandler _delete = delete;

        [HttpGet("record/{recordId:guid}")]
        [Authorize(Policy = "Prescriptions.View")]
        public async Task<IActionResult> GetByRecord(Guid recordId)
        {
            var items = await _prescriptionService.GetPrescriptionsByRecordIdAsync(recordId);
            return Ok(ApiResponse<IEnumerable<PrescriptionDto>>.Ok(_mapper.Map<IEnumerable<PrescriptionDto>>(items)));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "Prescriptions.View")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _prescriptionService.GetPrescriptionByIdAsync(id);
            if (entity == null)
                return NotFound(ApiResponse<object?>.Fail("Prescription not found"));

            return Ok(ApiResponse<PrescriptionDto>.Ok(_mapper.Map<PrescriptionDto>(entity)));
        }

        [HttpPost]
        [Authorize(Policy = "Prescriptions.Create")]
        public async Task<IActionResult> Create([FromBody] PrescriptionDto dto)
        {
            var created = await _create.HandleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ApiResponse<PrescriptionDto>.Ok(created, "Prescription created successfully"));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "Prescriptions.Edit")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PrescriptionDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ApiResponse<object?>.Fail("ID mismatch"));

            var updated = await _update.HandleAsync(dto);
            return Ok(ApiResponse<PrescriptionDto>.Ok(updated, "Prescription updated successfully"));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "Prescriptions.Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _delete.HandleAsync(id);
            return Ok(ApiResponse<object?>.Ok(null, "Prescription deleted successfully"));
        }
    }
}