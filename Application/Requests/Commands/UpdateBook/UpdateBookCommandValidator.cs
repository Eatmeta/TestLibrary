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

        RuleFor(updateBookCommand => updateBookCommand.IssueDate)
            .LessThan(updateBookCommand => updateBookCommand.ExpireDate)
            .WithMessage("Expire date must be after issue date");
        
        RuleFor(updateBookCommand => updateBookCommand.IssueDate)
            .NotNull()
            .When(updateBookCommand => updateBookCommand.ExpireDate != null)
            .WithMessage("Please set both issue and expire dates");
        
        RuleFor(createBookCommand => createBookCommand.Genre)
            .IsInEnum().WithMessage("Such a genre is not exist");
    }
}