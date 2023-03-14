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
        var resultBook = await Context.Books.Where(book => book.Id == BooksContextFactory.BookIdToDelete)
            .SingleOrDefaultAsync();
        var resultAuthors = await Context.Authors.Where(a => a.Books.Contains(resultBook)).ToListAsync();

        await handler.Handle(new DeleteBookCommand
        {
            Id = BooksContextFactory.BookIdToDelete
        }, CancellationToken.None);

        // Assert
        // Проверяем, что после удаления книги ее нет в таблице Книги.
        Assert.Null(Context.Books.SingleOrDefault(book => book.Id == BooksContextFactory.BookIdToDelete));

        foreach (var resultAuthor in resultAuthors)
        {
            // Проверяем, что после удаления книги у каждого автора она исчезла из списка его книг.
            Assert.False(resultAuthor.Books.Contains(resultBook));
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