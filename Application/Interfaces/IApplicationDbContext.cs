using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<AuthorBook> AuthorBooks { get; set; }
    DbSet<BookGenre> BookGenres { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}