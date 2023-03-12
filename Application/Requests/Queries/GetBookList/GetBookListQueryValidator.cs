using FluentValidation;

namespace Application.Requests.Queries.GetBookList;

public class GetBookListQueryValidator : AbstractValidator<GetBookListQuery>
{
    public GetBookListQueryValidator()
    {
    }
}