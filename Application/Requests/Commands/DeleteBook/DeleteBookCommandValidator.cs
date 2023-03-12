using FluentValidation;

namespace Application.Requests.Commands.DeleteBook;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(deleteBookCommand => deleteBookCommand.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required.");
    }
}