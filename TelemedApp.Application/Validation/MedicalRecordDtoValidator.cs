using FluentValidation;
using TelemedApp.Application.DTOs;

namespace TelemedApp.Application.Validation
{
    public class MedicalRecordDtoValidator : AbstractValidator<MedicalRecordDto>
    {
        public MedicalRecordDtoValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.Diagnosis)
                .NotEmpty().MaximumLength(500);

            RuleFor(x => x.Notes)
                .MaximumLength(1000);
        }
    }
}