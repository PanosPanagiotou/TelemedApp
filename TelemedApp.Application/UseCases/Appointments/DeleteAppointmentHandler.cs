using TelemedApp.Application.Exceptions;
using TelemedApp.Application.Interfaces;

namespace TelemedApp.Application.UseCases.Appointments
{
    public class DeleteAppointmentHandler(IAppointmentService service)
    {
        private readonly IAppointmentService _service = service;

        public async Task HandleAsync(Guid id)
        {
            if (!await _service.DeleteAppointmentAsync(id))
                throw new NotFoundException("Appointment not found");
        }
    }
}