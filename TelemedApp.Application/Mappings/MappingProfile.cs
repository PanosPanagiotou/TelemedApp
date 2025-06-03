using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
            // Add other mappings as needed
        }
    }
}