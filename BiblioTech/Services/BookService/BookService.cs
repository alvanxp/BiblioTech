using BiblioTech.Data;
using BiblioTech.Domain.Dto;
using BiblioTech.Domain.Entities;

namespace BiblioTech.Services.BookService;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<List<BookDto>> GetBooks()
    {
        var books = await bookRepository.GetBooks();
        return books.Select(book => new BookDto(book.Id,book.Title, book.Author, book.Genre, book.Description, book.PublishDate, book.Price, book.ISBN)).ToList();
    }

    public async Task<ResultDto<bool>> AddBook(BookDto? book)
    {
        if (book == null)
        {
            return new ResultDto<bool>()
            {
                Success = false,
                Message = "Book is null."
            };
        }
        var newBook = new Book
        {
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            Description = book.Description,
            PublishDate = book.PublishDate,
            Price = book.Price,
            ISBN = book.ISBN
        };
        var result =  await bookRepository.AddBook(newBook);
        return new ResultDto<bool>()
        {
            Success = result,
            Message =  "Book added successfully." 
        };
    }

    public async Task<BookDto?> GetBookById(int i)
    {
        var book = await bookRepository.GetBookById(i);
        return new BookDto(book.Id,book.Title, book.Author, book.Genre, book.Description, book.PublishDate, book.Price, book.ISBN);
    }
}