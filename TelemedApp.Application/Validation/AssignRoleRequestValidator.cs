using FluentValidation;
using TelemedApp.Application.Requests.Admin;

namespace TelemedApp.Application.Validation
{
    public class AssignRoleRequestValidator : AbstractValidator<AssignRoleRequest>
    {
        public AssignRoleRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Role)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}