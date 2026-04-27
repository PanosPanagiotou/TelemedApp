using FluentValidation;
using TelemedApp.Application.DTOs;

namespace TelemedApp.Application.Validation
{
    public class AppointmentDtoValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentDtoValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.ScheduledAt)
                .GreaterThan(DateTime.UtcNow.AddMinutes(-1))
                .WithMessage("Scheduled time must be in the future.");

            RuleFor(x => x.Notes)
                .MaximumLength(500);
        }
    }
}