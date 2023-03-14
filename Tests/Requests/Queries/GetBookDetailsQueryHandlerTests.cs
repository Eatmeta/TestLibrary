using Application.Interfaces;
using Application.Requests.Queries.GetBookDetails;
using AutoMapper;
using Domain.Models;
using Shouldly;
using Tests.Common;

namespace Tests.Requests.Queries;

[Collection("QueryCollection")]
public class GetBookDetailsQueryHandlerTests
{
    private readonly IApplicationDbContext Context;
    private readonly IMapper Mapper;

    public GetBookDetailsQueryHandlerTests(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetBookDetailsQueryHandler_Success()
    {
        // Arrange
        var handler = new GetBookDetailsQueryHandler(Context, Mapper);

        // Act
        var result = await handler.Handle(
            new GetBookDetailsQuery
            {
                Id = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb")
            },
            CancellationToken.None);

        // Assert
        result.ShouldBeOfType<BookDetailsDto>();
        result.Isbn.ShouldBe("9783161484100");
        result.Title.ShouldBe("Eugene Onegin");
        result.Genre.ShouldBe(GenreEnum.Novel);
        result.Description.ShouldBe("Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general.");
        result.IssueDate.ShouldBe(null);
        result.ExpireDate.ShouldBe(null);
        result.Authors[0].Id.ShouldBe(new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"));
        result.Authors[0].FirstName.ShouldBe("Alexander");
        result.Authors[0].LastName.ShouldBe("Pushkin");
        result.Authors[0].BirthDate.ShouldBe(new DateOnly(1799, 6, 6));

    }
}
