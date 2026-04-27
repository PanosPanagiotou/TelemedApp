using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelemedApp.API.Models;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Application.UseCases.Doctors;

namespace TelemedApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController(
        IDoctorService doctorService,
        IMapper mapper,
        CreateDoctorHandler create,
        UpdateDoctorHandler update,
        DeleteDoctorHandler delete) : ControllerBase
    {
        private readonly IDoctorService _doctorService = doctorService;
        private readonly IMapper _mapper = mapper;

        private readonly CreateDoctorHandler _create = create;
        private readonly UpdateDoctorHandler _update = update;
        private readonly DeleteDoctorHandler _delete = delete;

        [HttpGet]
        [Authorize(Policy = "Doctors.View")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _doctorService.GetAllDoctorsAsync();
            return Ok(ApiResponse<IEnumerable<DoctorDto>>.Ok(_mapper.Map<IEnumerable<DoctorDto>>(items)));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "Doctors.View")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _doctorService.GetDoctorByIdAsync(id);
            if (entity == null)
                return NotFound(ApiResponse<object?>.Fail("Doctor not found"));

            return Ok(ApiResponse<DoctorDto>.Ok(_mapper.Map<DoctorDto>(entity)));
        }

        [HttpPost]
        [Authorize(Policy = "Doctors.Create")]
        public async Task<IActionResult> Create([FromBody] DoctorDto dto)
        {
            var created = await _create.HandleAsync(dto);
            return CreatedAtAction(nameof(GetById),new { id = created.Id },ApiResponse<DoctorDto>.Ok(created, "Doctor created successfully"));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "Doctors.Edit")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DoctorDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ApiResponse<object?>.Fail("ID mismatch"));

            var updated = await _update.HandleAsync(dto);
            return Ok(ApiResponse<DoctorDto>.Ok(updated, "Doctor updated successfully"));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "Doctors.Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _delete.HandleAsync(id);
            return Ok(ApiResponse<object?>.Ok(null, "Doctor deleted successfully"));
        }
    }
}