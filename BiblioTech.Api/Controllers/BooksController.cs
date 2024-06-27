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
        return Ok(books);
    }
    // GET
    [HttpGet("{id}")]
    [SwaggerOperation("GetBookById")]
    public async Task<IActionResult> Get(int id)
    {
        var book = await bookService.GetBookById(id);
        return Ok(book);
    }
    
    // POST
    [HttpPost]
    [SwaggerOperation("AddBook")]
    public async Task<IActionResult> Post([FromBody] BookDto book)
    {
        var result = await bookService.AddBook(book);
        return Created("", book);
    }
}