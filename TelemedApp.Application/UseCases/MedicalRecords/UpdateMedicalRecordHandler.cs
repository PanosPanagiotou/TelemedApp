using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Exceptions;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.UseCases.MedicalRecords
{
    public class UpdateMedicalRecordHandler(IMedicalRecordService recordService, IMapper mapper)
    {
        private readonly IMedicalRecordService _recordService = recordService;
        private readonly IMapper _mapper = mapper;

        public async Task<MedicalRecordDto> HandleAsync(MedicalRecordDto dto)
        {
            var record = _mapper.Map<MedicalRecord>(dto);
            var success = await _recordService.UpdateMedicalRecordAsync(record);

            if (!success)
                throw new NotFoundException("Medical record not found");

            var updated = await _recordService.GetRecordByIdAsync(dto.Id);
            return _mapper.Map<MedicalRecordDto>(updated);
        }
    }
}