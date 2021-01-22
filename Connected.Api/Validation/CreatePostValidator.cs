using Connected.Api.Posts.Commands;
using FluentValidation;

namespace Connected.Api.Validation
{
    public class CreatePostValidator : AbstractValidator<CreatePost>
    {
        public CreatePostValidator()
        {
            RuleFor(p => p.Body).NotEmpty();
        }
    }
}