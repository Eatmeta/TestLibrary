using Application.Requests.Queries.GetBookList;
using AutoMapper;
using Persistence;
using Shouldly;
using Tests.Common;

namespace Tests.Requests.Queries;

[Collection("QueryCollection")]
public class GetBookListQueryHandlerTests
{
    private readonly ApplicationDbContext Context;
    private readonly IMapper Mapper;

    public GetBookListQueryHandlerTests(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetBookListQueryHandler_Success()
    {
        // Arrange
        var handler = new GetBookListQueryHandler(Context, Mapper);

        // Act
        var result = await handler.Handle(
            new GetBookListQuery(), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<BookListVm>();
        result.Books.Count.ShouldBe(2);
    }
}