using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;
using TelemedApp.Application.Exceptions;

namespace TelemedApp.Application.UseCases.Doctors
{
    public class UpdateDoctorHandler(IDoctorService doctorService, IMapper mapper)
    {
        private readonly IDoctorService _doctorService = doctorService;
        private readonly IMapper _mapper = mapper;

        public async Task<DoctorDto> HandleAsync(DoctorDto dto)
        {
            var doctor = _mapper.Map<Doctor>(dto);
            var success = await _doctorService.UpdateDoctorAsync(doctor);

            if (!success)
                throw new NotFoundException("Doctor not found");

            var updated = await _doctorService.GetDoctorByIdAsync(dto.Id);
            return _mapper.Map<DoctorDto>(updated);
        }
    }
}