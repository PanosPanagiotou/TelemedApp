using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using AutoMapper;
using TelemedApp.Application.Exceptions;

namespace TelemedApp.Application.UseCases.Appointments
{
    public class CreateAppointmentHandler(IAppointmentService service, IMapper mapper)
    {
        private readonly IAppointmentService _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<AppointmentDto> HandleAsync(AppointmentDto dto)
        {
            var appointment = _mapper.Map<Domain.Entities.Appointment>(dto);

            var start = appointment.ScheduledAt;
            var end = appointment.ScheduledAt.AddMinutes(30);

            if (!await _service.IsDoctorAvailable(dto.DoctorId, start, end))
                throw new ConflictException("Doctor is not available");

            if (!await _service.IsPatientAvailable(dto.PatientId, start, end))
                throw new ConflictException("Patient is not available");


            var created = await _service.CreateAppointmentAsync(appointment);
            return _mapper.Map<AppointmentDto>(created);
        }
    }
}