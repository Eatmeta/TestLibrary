using Application.Requests.Commands.CreateBook;
using Application.Requests.Commands.DeleteBook;
using Application.Requests.Commands.UpdateBook;
using Application.Requests.Queries.GetBookDetails;
using Application.Requests.Queries.GetBookList;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Produces("application/json")]
public class BookController : BaseController
{
    private readonly IMapper _mapper;
    public BookController(IMapper mapper) => _mapper = mapper;

    /// <summary>
    /// Gets the list of books
    /// </summary>
    /// <returns>Returns BookListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BookListVm>> GetAll()
    {
        var query = new GetBookListQuery();
        var bookListVm = await Mediator.Send(query);

        return Ok(bookListVm);
    }

    /// <summary>
    /// Gets the book by id
    /// </summary>
    /// <param name="id">Book's id (guid)</param>
    /// <returns>Returns BookDetailsDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpGet("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BookDetailsDto>> Get(Guid id)
    {
        var query = new GetBookDetailsQuery
        {
            Id = id
        };

        var bookDetailsDto = await Mediator.Send(query);

        return Ok(bookDetailsDto);
    }

    /// <summary>
    /// Gets the book by isbn
    /// </summary>
    /// <param name="id">Book's isbn</param>
    /// <returns>Returns BookDetailsDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpGet("{isbn}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BookDetailsDto>> Get(string isbn)
    {
        var query = new GetBookDetailsQuery
        {
            Isbn = isbn
        };

        var bookDetailsDto = await Mediator.Send(query);

        return Ok(bookDetailsDto);
    }
    /// <summary>
    /// Creates the book
    /// </summary>
    /// <param name="createBookDto">CreateBookDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBookDto createBookDto)
    {
        var command = _mapper.Map<CreateBookCommand>(createBookDto);
        var bookId = await Mediator.Send(command);

        return Ok(bookId);
    }

    /// <summary>
    /// Updates the book
    /// </summary>
    /// <param name="updateBookDto">UpdateBookDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="200">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateBookDto updateBookDto)
    {
        var command = _mapper.Map<UpdateBookCommand>(updateBookDto);
        await Mediator.Send(command);

        var bookId = await Mediator.Send(command);

        return Ok(bookId);
    }

    /// <summary>
    /// Deletes the book by id
    /// </summary>
    /// <param name="id">Id of the book (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBookCommand
        {
            Id = id
        };

        await Mediator.Send(command);

        return NoContent();
    }
}