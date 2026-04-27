using AutoMapper;
using TelemedApp.Application.DTOs;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //
            // PATIENT
            //
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>()
                .ForMember(dest => dest.Appointments, opt => opt.Ignore())
                .ForMember(dest => dest.MedicalRecords, opt => opt.Ignore());

            //
            // APPOINTMENT
            //
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(d => d.PatientName, opt => opt.MapFrom(s => s.Patient != null ? s.Patient.FullName : ""))
                .ForMember(d => d.DoctorName, opt => opt.MapFrom(s => s.Doctor != null ? s.Doctor.FullName : ""));

            CreateMap<AppointmentDto, Appointment>()
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore());

            //
            // DOCTOR
            //
            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>()
                .ForMember(dest => dest.Appointments, opt => opt.Ignore());

            //
            // MEDICAL RECORD
            //
            CreateMap<MedicalRecord, MedicalRecordDto>();
            CreateMap<MedicalRecordDto, MedicalRecord>();

            //
            // PRESCRIPTION
            //
            CreateMap<Prescription, PrescriptionDto>();
            CreateMap<PrescriptionDto, Prescription>();
        }
    }
}