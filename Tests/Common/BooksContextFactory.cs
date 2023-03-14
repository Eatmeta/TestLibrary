using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Tests.Common;

public class BooksContextFactory
{
    public static Guid BookIdToDelete = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb");
    public static Guid BookIdToUpdate = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba");

    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();

        context.Books.AddRange(
            new Book
            {
                Id = BookIdToDelete,
                Isbn = "9783161484100",
                Title = "Eugene Onegin",
                Genre = GenreEnum.Novel,
                Description =
                    "Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general."
            },
            new Book
            {
                Id = BookIdToUpdate,
                Isbn = "9780552137034",
                Title = "Good Omens",
                Genre = GenreEnum.Comedy,
                Description =
                    "Internationally bestselling authors Neil Gaiman and Terry Pratchett teamed up to write this witty comedy about the birth Satan’s son and the coming of the End Times."
            });

        context.Authors.AddRange(
            new Author
            {
                Id = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                FirstName = "Alexander",
                LastName = "Pushkin",
                BirthDate = new DateOnly(1799, 6, 6)
            },
            new Author
            {
                Id = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"),
                FirstName = "Neil",
                LastName = "Gaiman",
                BirthDate = new DateOnly(1960, 11, 10)
            });


        context.AuthorsBooks.AddRange(
            new AuthorBook
            {
                AuthorId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                BookId = BookIdToDelete
            },
            new AuthorBook
            {
                AuthorId = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"),
                BookId = BookIdToUpdate
            });


        context.SaveChanges();
        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}