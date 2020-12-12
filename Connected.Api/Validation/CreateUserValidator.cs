using Connected.Api.Users.Commands;
using FluentValidation;

namespace Connected.Api.Validation
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(10);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(7);
        }
    }
}