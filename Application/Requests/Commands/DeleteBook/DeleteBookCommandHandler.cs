using Application.Exceptions;
using Application.Interfaces;
using Domain.Models;
using MediatR;

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
        
        _dbContext.Books.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}