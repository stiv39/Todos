using FluentValidation;

namespace Todos.Application.Todos.Commands.Create;

internal class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.IsCompleted).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
