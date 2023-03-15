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
        var resultById = await handler.Handle(
            new GetBookDetailsQuery
            {
                Id = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb")
            },
            CancellationToken.None);
        
        var resultByIsbn = await handler.Handle(
            new GetBookDetailsQuery
            {
                Isbn = "9783161484100"
            },
            CancellationToken.None);

        // Assert
        resultById.ShouldBeOfType<BookDetailsDto>();
        resultById.Isbn.ShouldBe("9783161484100");
        resultById.Title.ShouldBe("Eugene Onegin");
        resultById.Genre.ShouldBe(GenreEnum.Novel);
        resultById.Description.ShouldBe("Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general.");
        resultById.IssueDate.ShouldBe(null);
        resultById.ExpireDate.ShouldBe(null);
        resultById.Authors[0].Id.ShouldBe(new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"));
        resultById.Authors[0].FirstName.ShouldBe("Alexander");
        resultById.Authors[0].LastName.ShouldBe("Pushkin");
        resultById.Authors[0].BirthDate.ShouldBe(new DateOnly(1799, 6, 6));
        
        resultByIsbn.ShouldBeOfType<BookDetailsDto>();
        resultByIsbn.Id.ShouldBe(new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"));
        resultByIsbn.Title.ShouldBe("Eugene Onegin");
        resultByIsbn.Genre.ShouldBe(GenreEnum.Novel);
        resultByIsbn.Description.ShouldBe("Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general.");
        resultByIsbn.IssueDate.ShouldBe(null);
        resultByIsbn.ExpireDate.ShouldBe(null);
        resultByIsbn.Authors[0].Id.ShouldBe(new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"));
        resultByIsbn.Authors[0].FirstName.ShouldBe("Alexander");
        resultByIsbn.Authors[0].LastName.ShouldBe("Pushkin");
        resultByIsbn.Authors[0].BirthDate.ShouldBe(new DateOnly(1799, 6, 6));

    }
}
