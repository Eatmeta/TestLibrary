using FluentValidation;

namespace Application.Requests.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(createBookCommand => createBookCommand.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be up to 100 characters long.");
        
        RuleFor(createBookCommand => createBookCommand.Isbn)
            .NotEmpty().WithMessage("ISBN is required.")
            .Must(isIsbnValid).WithMessage("ISBN is not valid.");
    }
    
    public static bool isIsbnValid(string isbn)
    {
        if (isbn == null) return false;

        isbn = isbn.Replace("-", "").Replace(" ", "");

        return isbn.Length switch
        {
            10 => IsIsbn10Valid(isbn),
            13 => IsIsbn13Valid(isbn),
            _ => false
        };
    }
    
    private static bool IsIsbn13Valid(string isbn)
    {
        var sum = 0;
        foreach (var (index, digit) in isbn.Select((digit, index) => (index, digit)))
        {
            if (char.IsDigit(digit)) sum += (digit - '0') * (index % 2 == 0 ? 1 : 3);
            else return false;
        }
        return sum % 10 == 0;
    }
    
    private static bool IsIsbn10Valid(string isbn)
    {
        var sum = 0;
        for (var i = 0; i < 10; i++)
        {
            var c = isbn[i];
            
            if (i == 9 && c == 'X')
            {
                sum += 10;
            }
            
            else if (!char.IsDigit(c)) return false;

            else
            {
                sum += (c - '0') * (10 - i);
            }
        }

        return sum % 11 == 0;
    }
}