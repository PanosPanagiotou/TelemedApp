using FluentValidation;
using TelemedApp.Application.Requests.Auth;

namespace TelemedApp.Application.Validation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(12)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[0-9]")
                .Matches("[^a-zA-Z0-9]");

            RuleFor(x => x.FullName)
                .NotEmpty().MaximumLength(100);
        }
    }
}