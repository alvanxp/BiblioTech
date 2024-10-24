using BiblioTech.Domain.Dto;
using BiblioTech.Services.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BiblioTech.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController(IBookService bookService) : ControllerBase
{
    // GET
    [HttpGet]
   [SwaggerOperation("GetBooks")]
    public async Task<IActionResult> Get()
{
        var books = await bookService.GetBooks();
        if (!books.Success)
        {
            return NotFound(books.Message);
        }
        return Ok(books.Data);
    }
    // GET
    [HttpGet("{id}")]
    [SwaggerOperation("GetBookById")]
    public async Task<IActionResult> Get(int id)
    {
        var book = await bookService.GetBookById(id);
        if (!book.Success)
        {
            return NotFound(book.Message);
        }
        return Ok(book.Data);
    }
    
    // POST
    [HttpPost]
    [SwaggerOperation("AddBook")]
    public async Task<IActionResult> Post([FromBody] BookRequest book)
    {
        var result = await bookService.AddBook(book);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtRoute(new { id = result.Data.Id }, result.Data);
    }
}