using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.UseCases.MedicalRecords
{
    public class CreateMedicalRecordHandler(IMedicalRecordService recordService, IMapper mapper)
    {
        private readonly IMedicalRecordService _recordService = recordService;
        private readonly IMapper _mapper = mapper;

        public async Task<MedicalRecordDto> HandleAsync(MedicalRecordDto dto)
        {
            var record = _mapper.Map<MedicalRecord>(dto);
            var created = await _recordService.CreateMedicalRecordAsync(record);
            return _mapper.Map<MedicalRecordDto>(created);
        }
    }
}
