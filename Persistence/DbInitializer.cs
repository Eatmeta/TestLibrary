using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DbInitializer
{
    private readonly ModelBuilder _builder;

    public DbInitializer(ModelBuilder builder)
    {
        _builder = builder;
    }

    public void Seed()
    {
        _builder.Entity<Author>(a =>
        {
            a.HasData(new Author
            {
                Id = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                FirstName = "Alexander",
                LastName = "Pushkin",
                BirthDate = new DateOnly(1799, 6, 6)
            });
            a.HasData(new Author
            {
                Id = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"),
                FirstName = "Neil",
                LastName = "Gaiman",
                BirthDate = new DateOnly(1960, 11, 10)
            });
            a.HasData(new Author
            {
                Id = new Guid("6fff3331-3bdd-4ca2-a178-6a35fd653c59"),
                FirstName = "Terry",
                LastName = "Pratchett",
                BirthDate = new DateOnly(1948, 04, 28)
            });
            a.HasData(new Author
            {
                Id = new Guid("a1784ca2-887b-4132-3bdd-9935fd65dd55"),
                FirstName = "David",
                LastName = "Mitchell",
                BirthDate = new DateOnly(1974, 07, 14)
            });
        });

        _builder.Entity<Book>(b =>
        {
            b.HasData(new Book
            {
                Id = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"),
                Isbn = "9783161484100",
                Title = "Eugene Onegin",
                Description =
                    "Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general."
            });
            b.HasData(new Book
            {
                Id = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"),
                Isbn = "9780552137034",
                Title = "Good Omens",
                Description =
                    "Internationally bestselling authors Neil Gaiman and Terry Pratchett teamed up to write this witty comedy about the birth Satan’s son and the coming of the End Times."
            });
            b.HasData(new Book
            {
                Id = new Guid("150c81c6-2458-466e-907a-2df11325e2b8"),
                Isbn = "9781529324983",
                Title = "Cloud Atlas",
                Description =
                    "A postmodern visionary who is also a master of styles of genres, David Mitchell combines flat-out adventure, a Nabokovian lore of puzzles, a keen eye for character, and a taste for mind-bending philosophical and scientific speculation."
            });
        });

        _builder.Entity<Genre>(g =>
        {
            g.HasData(new Genre
            {
                Id = 1,
                Name = "Novel"
            });
            g.HasData(new Genre
            {
                Id = 2,
                Name = "Comedy"
            });
            g.HasData(new Genre
            {
                Id = 3,
                Name = "Science Fiction"
            });
            g.HasData(new Genre
            {
                Id = 4,
                Name = "Fantasy"
            });
        });

        _builder.Entity<BookGenre>(bg =>
        {
            bg.HasData(new BookGenre
            {
                Id = 1,
                BookId = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"),
                GenreId = 1
            });
            bg.HasData(new BookGenre
            {
                Id = 2,
                BookId = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"),
                GenreId = 2
            });
            bg.HasData(new BookGenre
            {
                Id = 3,
                BookId = new Guid("150c81c6-2458-466e-907a-2df11325e2b8"),
                GenreId = 3
            });
            bg.HasData(new BookGenre
            {
                Id = 4,
                BookId = new Guid("150c81c6-2458-466e-907a-2df11325e2b8"),
                GenreId = 4
            });
        });

        _builder.Entity<AuthorBook>(ab =>
        {
            ab.HasData(new AuthorBook
            {
                Id = 1,
                AuthorId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                BookId = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb")
            });
            ab.HasData(new AuthorBook
            {
                Id = 2,
                AuthorId = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"),
                BookId = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba")
            });
            ab.HasData(new AuthorBook
            {
                Id = 3,
                AuthorId = new Guid("6fff3331-3bdd-4ca2-a178-6a35fd653c59"),
                BookId = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba")
            });
            ab.HasData(new AuthorBook
            {
                Id = 4,
                AuthorId = new Guid("a1784ca2-887b-4132-3bdd-9935fd65dd55"),
                BookId = new Guid("150c81c6-2458-466e-907a-2df11325e2b8")
            });
        });
    }
}