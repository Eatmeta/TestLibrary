using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.CreateBook;

public class CreateBookCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public GenreEnum Genre { get; set; }
    public List<Author> Authors { get; set; } = new();
    public string? Description { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ExpireDate { get; set; }
}