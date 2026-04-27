using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Exceptions;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.UseCases.Prescriptions
{
    public class UpdatePrescriptionHandler(IPrescriptionService prescriptionService, IMapper mapper)
    {
        private readonly IPrescriptionService _prescriptionService = prescriptionService;
        private readonly IMapper _mapper = mapper;

        public async Task<PrescriptionDto> HandleAsync(PrescriptionDto dto)
        {
            var prescription = _mapper.Map<Prescription>(dto);
            var success = await _prescriptionService.UpdatePrescriptionAsync(prescription);

            if (!success)
                throw new NotFoundException("Prescription not found");

            var updated = await _prescriptionService.GetPrescriptionByIdAsync(dto.Id);
            return _mapper.Map<PrescriptionDto>(updated);
        }
    }
}