using TelemedApp.Application.Exceptions;
using TelemedApp.Application.Interfaces;

namespace TelemedApp.Application.UseCases.MedicalRecords
{
    public class DeleteMedicalRecordHandler(IMedicalRecordService recordService)
    {
        private readonly IMedicalRecordService _recordService = recordService;

        public async Task HandleAsync(Guid id)
        {
            if (!await _recordService.DeleteMedicalRecordAsync(id))
                throw new NotFoundException("Medical record not found");
        }
    }
}