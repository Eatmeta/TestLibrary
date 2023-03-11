using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.GetBookDetails;

public class GetBookDetailsQueryHandler : IRequestHandler<GetBookDetailsQuery, BookDetailsDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<BookDetailsDto> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Books
            .AsNoTracking()
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(
                book => book.Id == request.Id || book.Isbn == request.Isbn, cancellationToken);
        
        if (entity == null)
            throw new NotFoundException(nameof(Book), request.Id);

        return _mapper.Map<BookDetailsDto>(entity);
    }
}