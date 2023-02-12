using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command=> command.Model.Surname).NotEmpty().MinimumLength(3);
            RuleFor(command=> command.Model.Birthday.Date).NotEmpty().LessThan(System.DateTime.Now.Date);
        }
    }
}