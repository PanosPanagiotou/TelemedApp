using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.UseCases
{
    public class CreatePrescriptionHandler(IPrescriptionService prescriptionService, IMapper mapper)
    {
        private readonly IPrescriptionService _prescriptionService = prescriptionService;
        private readonly IMapper _mapper = mapper;

        public async Task<PrescriptionDto> HandleAsync(PrescriptionDto dto)
        {
            var prescription = _mapper.Map<Prescription>(dto);
            var created = await _prescriptionService.CreatePrescriptionAsync(prescription);
            return _mapper.Map<PrescriptionDto>(created);
        }
    }
}
