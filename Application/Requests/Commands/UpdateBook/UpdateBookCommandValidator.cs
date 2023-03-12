using Application.Requests.Commands.CreateBook;
using FluentValidation;

namespace Application.Requests.Commands.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(updateBookCommand => updateBookCommand.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required.");
        
        RuleFor(updateBookCommand => updateBookCommand.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be up to 100 characters long.");
        
        RuleFor(updateBookCommand => updateBookCommand.Isbn)
            .NotEmpty().WithMessage("ISBN is required.")
            .Must(CreateBookCommandValidator.isIsbnValid).WithMessage("ISBN is not valid.");
    }
}