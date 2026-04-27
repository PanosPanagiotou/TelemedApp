using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.UseCases.Patients
{
    public class CreatePatientHandler(IPatientService patientService, IMapper mapper)
    {
        private readonly IPatientService _patientService = patientService;
        private readonly IMapper _mapper = mapper;

        public async Task<PatientDto> HandleAsync(PatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            var created = await _patientService.CreatePatientAsync(patient);
            return _mapper.Map<PatientDto>(created);
        }
    }
}