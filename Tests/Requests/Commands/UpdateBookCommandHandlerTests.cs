using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Requests.Commands.UpdateBook;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Tests.Common;

namespace Tests.Requests.Commands;

public class UpdateBookCommandHandlerTests : TestCommandBase
{
// Тест для проверки успешного обновления книги.
    [Fact]
    public async Task UpdateBookCommandHandler_Success()
    {
        // Arrange
        var handler = new UpdateBookCommandHandler(Context);
        var bookIsbn = "9783161484100";
        var bookTitle = "testUpdate";
        var bookDescription = "testUpdate";
        var bookGenre = GenreEnum.Novel;
        var bookIssueDate = new DateOnly(2023, 1, 9);
        var bookExpireDate = new DateOnly(2023, 5, 9);
        var authorId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59");
        var authorFirstName = "Alexander2";
        var authorLastName = "Pushkin2";
        var authorBirthDate = new DateOnly(1799, 6, 6);
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
        await handler.Handle(new UpdateBookCommand
        {
            Id = BooksContextFactory.BookIdToUpdate,
            Isbn = bookIsbn,
            Title = bookTitle,
            Genre = bookGenre,
            Description = bookDescription,
            IssueDate = bookIssueDate,
            ExpireDate = bookExpireDate,
            Authors = new List<Author>(authors)
        }, CancellationToken.None);

        // Assert
        var resultBook = Context.Books
            .Include(b => b.Authors)
            .AsEnumerable()
            .SingleOrDefault(book =>
                book.Id == BooksContextFactory.BookIdToUpdate &&
                book.Isbn == bookIsbn &&
                book.Title == bookTitle &&
                book.Genre == bookGenre &&
                book.Description == bookDescription);

        // Проверяем, что после обновления книги в таблице Книги она есть и только одна.
        Assert.NotNull(resultBook);
        
        // Проверяем, что после обновления книги в таблице Авторы у всех автором тоже обновились нужные поля.
        foreach (var updatedAuthor in GetUpdatedAuthors(Context, authors))
        {
            foreach (var author in authors.Where(author => updatedAuthor.Id == author.Id))
            {
                Assert.True(updatedAuthor.FirstName == author.FirstName);
                Assert.True(updatedAuthor.LastName == author.LastName);
                Assert.True(updatedAuthor.BirthDate == author.BirthDate);
            }
        }

        // Проверяем, что после обновления книги в таблице Авторы нет дублирующихся авторов этой книги.
        Assert.NotNull(
            Context.Authors
                .AsEnumerable()
                .SingleOrDefault(author =>
                    author.Id == authorId &&
                    author.FirstName == authorFirstName &&
                    author.LastName == authorLastName &&
                    author.BirthDate == authorBirthDate));

        var resultAuthors = await Context.Authors
            .Where(a => a.Books.Contains(resultBook)).ToListAsync();

        foreach (var resultAuthor in resultAuthors)
        {
            // Проверяем, что после добавления книги у каждого автора она есть и только одна в списке его книг.
            Assert.NotNull(
                Context.Authors
                    .AsEnumerable()
                    .SingleOrDefault(author =>
                        author.Id == resultAuthor.Id &&
                        author.FirstName == resultAuthor.FirstName &&
                        author.LastName == resultAuthor.LastName &&
                        author.BirthDate == resultAuthor.BirthDate));
        }
    }
    
    // Тест для проверки случая, когда неправильный Id книги.
    [Fact]
    public async Task UpdateBookCommandHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new UpdateBookCommandHandler(Context);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new UpdateBookCommand
                {
                    Id = Guid.NewGuid()
                },
                CancellationToken.None));
    }
    
    private static IEnumerable<Author> GetUpdatedAuthors(IApplicationDbContext context, List<Author> requestAuthors)
    {
        var result = new List<Author>();

        foreach (var requestAuthor in requestAuthors)
        {
            var authorInDatabase = context.Authors.Find(requestAuthor.Id);

            if (authorInDatabase != null)
            {
                result.Add(authorInDatabase);
            }
            else
            {
                throw new NotFoundException(nameof(Author), requestAuthor.Id);
            }
        }
        return result;
    }
}