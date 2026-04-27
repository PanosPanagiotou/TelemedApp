using TelemedApp.Application.Exceptions;
using TelemedApp.Application.Interfaces;

namespace TelemedApp.Application.UseCases.Patients
{
    public class DeletePatientHandler(IPatientService patientService)
    {
        private readonly IPatientService _patientService = patientService;

        public async Task HandleAsync(Guid id)
        {
            if (!await _patientService.DeletePatientAsync(id))
                throw new NotFoundException("Patient not found");
        }
    }
}