using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateBookCommandHandler(IApplicationDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var authors = new List<Author>();

        foreach (var requestAuthor in request.Authors)
        {
            var authorInDatabase = await _dbContext.Authors.SingleOrDefaultAsync(a =>
                a.FirstName == requestAuthor.FirstName &&
                a.LastName == requestAuthor.LastName &&
                a.BirthDate == requestAuthor.BirthDate, cancellationToken: cancellationToken);

            if (authorInDatabase != null)
            {
                authors.Add(authorInDatabase);
            }
            else
            {
                authors.Add(requestAuthor);
                await _dbContext.Authors.AddAsync(requestAuthor, cancellationToken);
            }
        }

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Isbn = request.Isbn.Replace("-", "").Replace(" ", ""),
            Description = request.Description,
            Authors = authors,
            Genre = request.Genre
        };

        await _dbContext.Books.AddAsync(book, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}