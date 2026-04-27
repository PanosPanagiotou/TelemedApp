using FluentValidation;
using TelemedApp.Application.DTOs;

namespace TelemedApp.Application.Validation
{
    public class PatientDtoValidator : AbstractValidator<PatientDto>
    {
        public PatientDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow)
                .WithMessage("Birth date must be in the past.");

            RuleFor(x => x.Gender)
                .NotEmpty().MaximumLength(20);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^\+?[0-9]{7,15}$");

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.Address)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.InsuranceProvider)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.InsuranceNo)
                .NotEmpty().MaximumLength(50);

            RuleFor(x => x.MedicalRecordNumber)
                .NotEmpty().MaximumLength(50);

            RuleFor(x => x.NextOfKin)
                .MaximumLength(100);

            RuleFor(x => x.Notes)
                .MaximumLength(500);
        }
    }
}