using TelemedApp.Application.Exceptions;
using TelemedApp.Application.Interfaces;

namespace TelemedApp.Application.UseCases.Prescriptions
{
    public class DeletePrescriptionHandler(IPrescriptionService prescriptionService)
    {
        private readonly IPrescriptionService _prescriptionService = prescriptionService;

        public async Task HandleAsync(Guid id)
        {
            if (!await _prescriptionService.DeletePrescriptionAsync(id))
                throw new NotFoundException("Prescription not found");
        }
    }
}