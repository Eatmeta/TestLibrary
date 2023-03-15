using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteBookCommandHandler(IApplicationDbContext dbContext)
        => _dbContext = dbContext;

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    { 
        var entity = await _dbContext.Books.FindAsync(new object[] {request.Id}, cancellationToken);
        
        if (entity == null)
            throw new NotFoundException(nameof(Book), request.Id);

        var authorsToDelete = await _dbContext.Authors.Where(a =>
            a.Books.Contains(entity) &&
            a.Books.Count == 1).ToListAsync(cancellationToken: cancellationToken);
        
        foreach (var author in authorsToDelete)
        {
            _dbContext.Authors.Remove(author);
        }
        
        _dbContext.Books.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}