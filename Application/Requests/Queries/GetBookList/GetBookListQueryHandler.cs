using Application.Interfaces;
using Application.Requests.Queries.GetBookDetails;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.GetBookList;

public class GetBookListQueryHandler : IRequestHandler<GetBookListQuery, BookListVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookListQueryHandler(IApplicationDbContext dbContext, IMapper mapper, IMediator mediator) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<BookListVm> Handle(GetBookListQuery request, CancellationToken cancellationToken)
    {
        var booksQuery = await _dbContext.Books
            .AsNoTracking()
            .Include(b => b.Authors)
            .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return new BookListVm {Books = booksQuery};
    }
}