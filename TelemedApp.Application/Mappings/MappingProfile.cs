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

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();

            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordDto>().ReverseMap();
            CreateMap<Prescription, PrescriptionDto>().ReverseMap();
        }
    }
}