using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Exceptions;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.UseCases.Patients
{
    public class UpdatePatientHandler(IPatientService patientService, IMapper mapper)
    {
        private readonly IPatientService _patientService = patientService;
        private readonly IMapper _mapper = mapper;

        public async Task<PatientDto> HandleAsync(PatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            var success = await _patientService.UpdatePatientAsync(patient);

            if (!success)
                throw new NotFoundException("Patient not found");

            var updated = await _patientService.GetPatientByIdAsync(dto.Id);
            return _mapper.Map<PatientDto>(updated);
        }
    }
}