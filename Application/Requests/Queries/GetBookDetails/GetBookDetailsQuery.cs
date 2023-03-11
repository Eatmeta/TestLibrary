using MediatR;

namespace Application.Requests.Queries.GetBookDetails;

public class GetBookDetailsQuery : IRequest<BookDetailsDto>
{
    public Guid Id { get; set; }
    public string Isbn { get; set; }
}