using Application.Requests.Commands.CreateBook;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Tests.Common;

namespace Tests.Requests.Commands;

public class CreateBookCommandHandlerTests : TestCommandBase
{
// Тест для проверки успешного создания книги.
    [Fact]
    public async Task CreateBookCommandHandler_Success()
    {
        // Arrange
        var handler = new CreateBookCommandHandler(Context);
        var bookId = new Guid("150c81c6-2458-466e-907a-2df11325e2b8");
        var bookIsbn = "9783161484100";
        var bookTitle = "testCreate";
        var bookDescription = "testCreate";
        var bookGenre = GenreEnum.Fantasy;
        var authorId = new Guid("a1784ca2-887b-4132-3bdd-9935fd65dd55");
        var authorFirstName = "David";
        var authorLastName = "Mitchell";
        var authorBirthDate = new DateOnly(1974, 07, 14);
        var authors = new List<Author>
        {
            new()
            {
                Id = authorId,
                FirstName = authorFirstName,
                LastName = authorLastName,
                BirthDate = authorBirthDate
            }
        };

        // Act
        var resultBookId = await handler.Handle(
            new CreateBookCommand
            {
                Id = bookId,
                Isbn = bookIsbn,
                Title = bookTitle,
                Description = bookDescription,
                Genre = bookGenre,
                Authors = new List<Author>(authors)
            },
            CancellationToken.None);

        // Assert
        var resultBook = Context.Books
            .Include(b => b.Authors)
            .AsEnumerable()
            .SingleOrDefault(book =>
            book.Id == resultBookId &&
            book.Isbn == bookIsbn &&
            book.Title == bookTitle &&
            book.Genre == bookGenre &&
            book.Description == bookDescription &&
            book.Authors.SequenceEqual(authors));
        
        // Проверяем, что после добавления книги в таблице Книги она есть и только одна.
        Assert.NotNull(resultBook);

        // Проверяем, что после добавления книги в таблице Авторы нет дублирующихся авторов этой книги.
        Assert.NotNull(
            await Context.Authors.SingleOrDefaultAsync(author =>
                author.Id == authorId &&
                author.FirstName == authorFirstName &&
                author.LastName == authorLastName &&
                author.BirthDate == authorBirthDate));
        
        var resultAuthors = await Context.Authors.Where(a => a.Books.Contains(resultBook)).ToListAsync();

        foreach (var resultAuthor in resultAuthors)
        {
            // Проверяем, что после добавления книги у каждого автора она есть и только одна в списке его книг.
            Assert.NotNull(
                await Context.Authors.SingleOrDefaultAsync(author =>
                    author.Id == resultAuthor.Id &&
                    author.FirstName == resultAuthor.FirstName &&
                    author.LastName == resultAuthor.LastName &&
                    author.BirthDate == resultAuthor.BirthDate));
        }
    }
}