﻿using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateBookCommandHandler(IApplicationDbContext dbContext) 
        => _dbContext = dbContext;
    
    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Isbn = request.Isbn.Replace("-", "").Replace(" ", ""),
            Description = request.Description,
            Authors = request.Authors,
            Genre = request.Genre
        };
        
        await _dbContext.Books.AddAsync(book, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}