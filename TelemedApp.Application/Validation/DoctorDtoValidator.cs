using FluentValidation;
using TelemedApp.Application.DTOs;

namespace TelemedApp.Application.Validation
{
    public class DoctorDtoValidator : AbstractValidator<DoctorDto>
    {
        public DoctorDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.Specialty)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^\+?[0-9]{7,15}$");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().MaximumLength(50);

            RuleFor(x => x.Notes)
                .MaximumLength(500);
        }
    }
}