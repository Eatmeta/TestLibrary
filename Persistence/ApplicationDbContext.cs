using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfigurations;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorBookConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new BookGenreConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>().HasData(new Book
        {
            BookId = 1,
            Isbn = "9783161484100",
            Title = "Eugene Onegin",
            Description =
                "Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general."
        });
        
        modelBuilder.Entity<Book>().HasData(new Book
        {
            BookId = 2,
            Isbn = "9780552137034",
            Title = "Good Omens",
            Description =
                "Internationally bestselling authors Neil Gaiman and Terry Pratchett teamed up to write this witty comedy about the birth Satan’s son and the coming of the End Times."
        });
        
        modelBuilder.Entity<Book>().HasData(new Book
        {
            BookId = 3,
            Isbn = "9781529324983",
            Title = "Cloud Atlas",
            Description =
                "A postmodern visionary who is also a master of styles of genres, David Mitchell combines flat-out adventure, a Nabokovian lore of puzzles, a keen eye for character, and a taste for mind-bending philosophical and scientific speculation."
        });
        
        modelBuilder.Entity<Author>().HasData(new Author
        {
            AuthorId = 1,
            FirstName = "Alexander",
            LastName = "Pushkin",
            BirthDate = new DateOnly(1799, 6, 6)
        });
        
        modelBuilder.Entity<Author>().HasData(new Author
        {
            AuthorId = 2,
            FirstName = "Neil",
            LastName = "Gaiman",
            BirthDate = new DateOnly(1960, 11, 10)
        });
        
        modelBuilder.Entity<Author>().HasData(new Author
        {
            AuthorId = 3,
            FirstName = "Terry",
            LastName = "Pratchett",
            BirthDate = new DateOnly(1948, 04, 28)
        });
        
        modelBuilder.Entity<Author>().HasData(new Author
        {
            AuthorId = 4,
            FirstName = "David",
            LastName = "Mitchell",
            BirthDate = new DateOnly(1974, 07, 14)
        });
        
        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            GenreId = 1,
            Name = "Novel"
        });
        
        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            GenreId = 2,
            Name = "Comedy"
        });
        
        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            GenreId = 3,
            Name = "Science Fiction"
        });
        
        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            GenreId = 4,
            Name = "Fantasy"
        });
        
        modelBuilder.Entity<AuthorBook>().HasData(new AuthorBook
        {
            AuthorBookId = 1,
            AuthorId = 1,
            BookId = 1
        });
        
        modelBuilder.Entity<AuthorBook>().HasData(new AuthorBook
        {
            AuthorBookId = 2,
            AuthorId = 2,
            BookId = 2
        });
        
        modelBuilder.Entity<AuthorBook>().HasData(new AuthorBook
        {
            AuthorBookId = 3,
            AuthorId = 3,
            BookId = 2
        });
        
        modelBuilder.Entity<AuthorBook>().HasData(new AuthorBook
        {
            AuthorBookId = 4,
            AuthorId = 4,
            BookId = 3
        });
        
        modelBuilder.Entity<BookGenre>().HasData(new BookGenre
        {
            BookGenreId = 1,
            BookId = 1,
            GenreId = 1
        });
        
        modelBuilder.Entity<BookGenre>().HasData(new BookGenre
        {
            BookGenreId = 2,
            BookId = 2,
            GenreId = 2
        });
        
        modelBuilder.Entity<BookGenre>().HasData(new BookGenre
        {
            BookGenreId = 3,
            BookId = 3,
            GenreId = 3
        });
        
        modelBuilder.Entity<BookGenre>().HasData(new BookGenre
        {
            BookGenreId = 4,
            BookId = 3,
            GenreId = 4
        });
    }
}