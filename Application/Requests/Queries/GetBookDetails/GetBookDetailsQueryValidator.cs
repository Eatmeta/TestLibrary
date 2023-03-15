using FluentValidation;

namespace Application.Requests.Queries.GetBookDetails;

public class GetBookDetailsQueryValidator : AbstractValidator<GetBookDetailsQuery>
{
    public GetBookDetailsQueryValidator()
    {
        RuleFor(getBookDetailsQuery => getBookDetailsQuery.Id)
            .NotEmpty()
            .When(getBookDetailsQuery => string.IsNullOrEmpty(getBookDetailsQuery.Isbn))
            .WithMessage("Id is required");
        
        RuleFor(getBookDetailsQuery => getBookDetailsQuery.Isbn)
            .NotEmpty()
            .When(getBookDetailsQuery => string.IsNullOrEmpty(getBookDetailsQuery.Id.ToString()))
            .WithMessage("Isbn is required");
    }
}