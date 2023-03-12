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
    
    [HttpGet]
    public async Task<ActionResult<BookListVm>> GetAll()
    {
        var query = new GetBookListQuery();
        var bookListVm = await Mediator.Send(query);

        return Ok(bookListVm);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookDetailsDto>> Get(Guid id)
    {
        var query = new GetBookDetailsQuery
        {
            Id = id
        };
        
        var bookDetailsDto = await Mediator.Send(query);
        
        return Ok(bookDetailsDto);
    }
    
    [HttpGet("{isbn}")]
    public async Task<ActionResult<BookDetailsDto>> Get(string isbn)
    {
        var query = new GetBookDetailsQuery
        {
            Isbn = isbn
        };
        
        var bookDetailsDto = await Mediator.Send(query);
        
        return Ok(bookDetailsDto);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBookDto createBookDto)
    {
        var command = _mapper.Map<CreateBookCommand>(createBookDto);
        var bookId = await Mediator.Send(command);
        
        return Ok(bookId);
    }
    
    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update([FromBody] UpdateBookDto updateBookDto)
    {
        var command = _mapper.Map<UpdateBookCommand>(updateBookDto);
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
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