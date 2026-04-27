using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.UseCases.Doctors
{
    public class CreateDoctorHandler(IDoctorService doctorService, IMapper mapper)
    {
        private readonly IDoctorService _doctorService = doctorService;
        private readonly IMapper _mapper = mapper;

        public async Task<DoctorDto> HandleAsync(DoctorDto dto)
        {
            var doctor = _mapper.Map<Doctor>(dto);
            var created = await _doctorService.CreateDoctorAsync(doctor);
            return _mapper.Map<DoctorDto>(created);
        }
    }
}
