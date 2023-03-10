using Application.Requests.Queries.GetBookDetails;

namespace Application.Requests.Queries.GetBookList;

public class BookListVm
{
    public IList<BookDetailsDto> Books { get; set; }
}