using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Enums;
using AutoMapper;

namespace TelemedApp.Application.UseCases
{
    public class CreateAppointmentHandler(IAppointmentService appointmentService, IMapper mapper)
    {
        private readonly IAppointmentService _appointmentService = appointmentService;
        private readonly IMapper _mapper = mapper;

        public async Task<AppointmentDto> HandleAsync(AppointmentDto dto)
        {
            var appointment = _mapper.Map<Appointment>(dto);
            appointment.Status = AppointmentStatus.Scheduled;

            var created = await _appointmentService.CreateAppointmentAsync(appointment);
            return _mapper.Map<AppointmentDto>(created);
        }
    }
}
