using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;

namespace TelemedApp.Application.UseCases;

public class CreatePatientHandler(IUnitOfWork unitOfWork, IMapper mapper)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<PatientDto> HandleAsync(PatientDto patientDto)
    {
        var patient = _mapper.Map<Patient>(patientDto);
        await _unitOfWork.Patients.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PatientDto>(patient);
    }
}