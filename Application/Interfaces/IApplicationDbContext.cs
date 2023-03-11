using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<AuthorBook> AuthorsBooks { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}