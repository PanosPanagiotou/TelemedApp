using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Enums;
using TelemedApp.Domain.Interfaces;

namespace TelemedApp.Infrastructure.Services
{
    public class AppointmentService(IUnitOfWork uow) : IAppointmentService
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task<bool> IsDoctorAvailable(Guid doctorId, DateTime start, DateTime end)
        {
            var appointments = await _uow.Appointments.GetAllAsync();

            return !appointments.Any(a =>
                a.DoctorId == doctorId &&
                a.Status == AppointmentStatus.Scheduled &&
                a.ScheduledAt >= start &&
                a.ScheduledAt < end);
        }

        public async Task<bool> IsPatientAvailable(Guid patientId, DateTime start, DateTime end)
        {
            var appointments = await _uow.Appointments.GetAllAsync();

            return !appointments.Any(a =>
                a.PatientId == patientId &&
                a.Status == AppointmentStatus.Scheduled &&
                a.ScheduledAt >= start &&
                a.ScheduledAt < end);
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _uow.Appointments.AddAsync(appointment);
            await _uow.SaveChangesAsync();
            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync(Guid? doctorId = null, Guid? patientId = null)
        {
            var all = await _uow.Appointments.GetAllAsync();

            if (doctorId.HasValue)
                all = [.. all.Where(a => a.DoctorId == doctorId.Value)];

            if (patientId.HasValue)
                all = [.. all.Where(a => a.PatientId == patientId.Value)];

            return all;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
        {
            return await _uow.Appointments.GetAsync(id);
        }


        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _uow.Appointments.GetAllAsync();
        }

        public async Task<bool> CancelAppointmentAsync(Guid appointmentId)
        {
            var appointment = await _uow.Appointments.GetAsync(appointmentId);
            if (appointment == null)
                return false;

            appointment.Status = AppointmentStatus.Cancelled;
            _uow.Appointments.Update(appointment);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<Appointment> RescheduleAppointmentAsync(Appointment appointment)
        {
            var existing = await _uow.Appointments.GetAsync(appointment.Id) ?? throw new Exception("Appointment not found");
            existing.ScheduledAt = appointment.ScheduledAt;
            existing.Notes = appointment.Notes;

            _uow.Appointments.Update(existing);
            await _uow.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            var existing = await _uow.Appointments.GetAsync(appointment.Id);
            if (existing == null)
                return false;

            _uow.Appointments.Update(appointment);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAppointmentAsync(Guid id)
        {
            var existing = await _uow.Appointments.GetAsync(id);
            if (existing == null)
                return false;

            await _uow.Appointments.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}