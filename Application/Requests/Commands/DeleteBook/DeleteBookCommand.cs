using MediatR;

namespace Application.Requests.Commands.DeleteBook;

public class DeleteBookCommand : IRequest
{
    public Guid Id { get; set; }
}