using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateBookCommandHandler(IApplicationDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<Guid> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);
        
        if (entity.Isbn != request.Isbn && _dbContext.Books.Any(b => b.Isbn == request.Isbn))
        {
            throw new DublicateIsbnException(request.Isbn);
        }

        if (entity == null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        var requestAuthorIds = request.Authors.Select(a => a.Id).ToList();

        foreach (var requestAuthorId in requestAuthorIds.Where(requestAuthorId =>
                     !_dbContext.Authors.Any(a => a.Id == requestAuthorId)))
        {
            throw new NotFoundException(nameof(Author), requestAuthorId);
        }

        entity.Isbn = request.Isbn.Replace("-", "").Replace(" ", "");
        entity.Title = request.Title;
        entity.Genre = request.Genre;
        entity.Description = request.Description;
        entity.IssueDate = request.IssueDate;
        entity.ExpireDate = request.ExpireDate;
        
        foreach (var requestAuthor in request.Authors)
        {
            var author = await _dbContext.Authors.FindAsync(new object[] {requestAuthor.Id}, cancellationToken);
            author.FirstName = requestAuthor.FirstName;
            author.LastName = requestAuthor.LastName;
            author.BirthDate = requestAuthor.BirthDate;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}