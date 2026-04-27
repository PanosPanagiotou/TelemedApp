using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using AutoMapper;
using TelemedApp.Application.Exceptions;

namespace TelemedApp.Application.UseCases.Appointments
{
    public class UpdateAppointmentHandler(IAppointmentService service, IMapper mapper)
    {
        private readonly IAppointmentService _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<AppointmentDto?> HandleAsync(AppointmentDto dto)
        {
            var appointment = _mapper.Map<Domain.Entities.Appointment>(dto);
            var success = await _service.UpdateAppointmentAsync(appointment);

            if (!success)
                throw new NotFoundException("Appointment not found");

            var updated = await _service.GetAppointmentByIdAsync(dto.Id);
            return _mapper.Map<AppointmentDto>(updated);
        }
    }
}