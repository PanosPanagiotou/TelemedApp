using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<bool> IsDoctorAvailable(Guid doctorId, DateTime start, DateTime end);
        Task<bool> IsPatientAvailable(Guid patientId, DateTime start, DateTime end);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync(Guid? doctorId = null, Guid? patientId = null);
        Task<bool> CancelAppointmentAsync(Guid appointmentId);
        Task<Appointment> RescheduleAppointmentAsync(Appointment appointment);
    }
}