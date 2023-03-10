using MediatR;

namespace Application.Requests.Queries.GetBookList;

public class GetBookListQuery : IRequest<BookListVm>
{
}