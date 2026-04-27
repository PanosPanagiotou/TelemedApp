using TelemedApp.Application.Interfaces;
using TelemedApp.Application.Exceptions;

namespace TelemedApp.Application.UseCases.Doctors
{
    public class DeleteDoctorHandler(IDoctorService doctorService)
    {
        private readonly IDoctorService _doctorService = doctorService;

        public async Task HandleAsync(Guid id)
        {
            if (!await _doctorService.DeleteDoctorAsync(id))
                throw new NotFoundException("Doctor not found");
        }
    }
}