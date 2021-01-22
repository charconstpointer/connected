using Connected.Api.Groups.Commands;
using FluentValidation;

namespace Connected.Api.Validation
{
    public class CreateNewGroupValidator : AbstractValidator<CreateGroup>
    {
        public CreateNewGroupValidator()
        {
            RuleFor(c => c.Name.Length)
                .NotEmpty()
                .GreaterThan(2);
        }
    }
}