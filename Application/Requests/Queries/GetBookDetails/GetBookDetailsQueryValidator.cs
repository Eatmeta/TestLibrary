using FluentValidation;

namespace Application.Requests.Queries.GetBookDetails;

public class GetBookDetailsQueryValidator : AbstractValidator<GetBookDetailsQuery>
{
    public GetBookDetailsQueryValidator()
    {
        RuleFor(getBookDetailsQuery => getBookDetailsQuery)
            .Must(getBookDetailsQuery => !string.IsNullOrEmpty(getBookDetailsQuery.Id.ToString())
                                         || !string.IsNullOrEmpty(getBookDetailsQuery.Isbn))
            .WithMessage("Either Id or ISBN is required");
    }
}