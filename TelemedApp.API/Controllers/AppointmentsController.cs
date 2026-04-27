using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelemedApp.API.Models;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Application.UseCases.Appointments;

namespace TelemedApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(
        IAppointmentService appointmentService,
        IMapper mapper,
        CreateAppointmentHandler create,
        UpdateAppointmentHandler update,
        DeleteAppointmentHandler delete) : ControllerBase
    {
        private readonly IAppointmentService _appointmentService = appointmentService;
        private readonly IMapper _mapper = mapper;

        private readonly CreateAppointmentHandler _create = create;
        private readonly UpdateAppointmentHandler _update = update;
        private readonly DeleteAppointmentHandler _delete = delete;

        [HttpGet]
        [Authorize(Policy = "Appointments.View")]
        public async Task<IActionResult> GetAll(
            [FromQuery] Guid? doctorId = null,
            [FromQuery] Guid? patientId = null)
        {
            var items = await _appointmentService.GetAppointmentsAsync(doctorId, patientId);
            var mapped = _mapper.Map<IEnumerable<AppointmentDto>>(items) ?? [];
            return Ok(ApiResponse<IEnumerable<AppointmentDto>>.Ok(mapped));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "Appointments.View")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _appointmentService.GetAppointmentByIdAsync(id);
            if (entity == null)
                return NotFound(ApiResponse<object?>.Fail("Appointment not found"));

            return Ok(ApiResponse<AppointmentDto>.Ok(_mapper.Map<AppointmentDto>(entity)));
        }

        [HttpPost]
        [Authorize(Policy = "Appointments.Create")]
        public async Task<IActionResult> Create([FromBody] AppointmentDto dto)
        {
            var created = await _create.HandleAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                ApiResponse<AppointmentDto>.Ok(created, "Appointment created successfully")
            );
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "Appointments.Edit")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AppointmentDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ApiResponse<object?>.Fail("ID mismatch"));

            var updated = await _update.HandleAsync(dto);
            if (updated == null)
                return NotFound(ApiResponse<object?>.Fail("Appointment not found"));

            return Ok(ApiResponse<AppointmentDto>.Ok(updated, "Appointment updated successfully"));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "Appointments.Delete")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _delete.HandleAsync(id);
            return Ok(ApiResponse<object?>.Ok(null, "Appointment deleted successfully"));
        }
    }
}