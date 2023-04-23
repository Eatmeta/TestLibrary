using Application.Common.Exceptions;
using Application.Requests.Commands.DeleteBook;
using Microsoft.EntityFrameworkCore;
using Tests.Common;

namespace Tests.Requests.Commands;

public class DeleteBookCommandHandlerTests : TestCommandBase
{
// Тест для проверки успешного удаления книги.
    [Fact]
    public async Task DeleteBookCommandHandler_Success()
    {
        var handler = new DeleteBookCommandHandler(Context);
        var resultBook = await Context.Books
            .Where(book => book.Id == BooksContextFactory.BookIdToDelete)
            .SingleOrDefaultAsync();
        var resultAuthors = await Context.Authors
            .Where(a => a.Books.Contains(resultBook))
            .ToListAsync();

        await handler.Handle(new DeleteBookCommand
        {
            Id = BooksContextFactory.BookIdToDelete
        }, CancellationToken.None);

        // Assert
        // Проверяем, что после удаления книги ее нет в таблице Книги.
        Assert.Null(Context.Books.SingleOrDefault(book => book.Id == BooksContextFactory.BookIdToDelete));

        // Проверяем, что после удаления книги у авторов этой книги ее больше нет в списке их книг.
        // А если у автора была только одна эта книга, то автор удаляется из базы.
        foreach (var resultAuthor in resultAuthors.SelectMany(_ => resultAuthors))
        {
            var author = await Context.Authors.FindAsync(resultAuthor.Id);
            if (author != null)
                Assert.DoesNotContain(resultBook, author.Books);
        }
    }

// Тест для проверки случая, когда неправильный Id книги.
    [Fact]
    public async Task DeleteBookCommandHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new DeleteBookCommandHandler(Context);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new DeleteBookCommand
                {
                    Id = Guid.NewGuid()
                },
                CancellationToken.None));
    }
}