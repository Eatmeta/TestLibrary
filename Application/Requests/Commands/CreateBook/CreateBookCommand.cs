using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.CreateBook;

public class CreateBookCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public List<Genre> Genres { get; set; } = new();
    public List<Author> Authors { get; set; } = new();
    public string? Description { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
}