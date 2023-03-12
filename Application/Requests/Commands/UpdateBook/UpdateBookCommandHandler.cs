using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateBookCommandHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }
        
        entity.Isbn = request.Isbn.Replace("-", "").Replace(" ", "");
        entity.Title = request.Title;
        entity.Genre = request.Genre;
        entity.Authors = request.Authors;
        entity.Description = request.Description;
        entity.IssueDate = request.IssueDate;
        entity.ExpireDate = request.ExpireDate;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}