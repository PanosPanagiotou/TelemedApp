using FluentValidation;
using TelemedApp.Application.DTOs;

namespace TelemedApp.Application.Validation
{
    public class PrescriptionDtoValidator : AbstractValidator<PrescriptionDto>
    {
        public PrescriptionDtoValidator()
        {
            RuleFor(x => x.MedicalRecordId)
                .NotEmpty();

            RuleFor(x => x.Medication)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.Dosage)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.Instructions)
                .NotEmpty().MaximumLength(1000);
        }
    }
}