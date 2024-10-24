using BiblioTech.Data;
using BiblioTech.Domain.Dto;
using BiblioTech.Domain.Entities;

namespace BiblioTech.Services.BookService;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<ResultDto<List<BookResponse>>> GetBooks()
    {
        //TODO: use a mapper, instead of manual mapping
        var books = await bookRepository.GetBooks();
        if (books is null)
        {
            return new ResultDto<List<BookResponse>>()
            {
                Data = Enumerable.Empty<BookResponse>().ToList(),
                Success = false,
                Message = "No books found"
            };
        }
        return new ResultDto<List<BookResponse>>()
        {
            Data = books.Select(book => new BookResponse(book.Id, book.Title, book.Author, book.Genre, book.Description, book.PublishDate, book.Price, book.ISBN)).ToList(),
            Success = true,
            Message = "Books found"
        };
    }

    public async Task<ResultDto<BookResponse>> AddBook(BookRequest? book)
    {
        ArgumentNullException.ThrowIfNull(book);
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
        bool? success = await bookRepository.AddBook(newBook);
        if (!success.GetValueOrDefault())
        {
            return new ResultDto<BookResponse>
            {
                Success = false,
                Message = "Failed to add book"
            };
        }
        return new ResultDto<BookResponse>
        {
            Data = new BookResponse(newBook.Id, newBook.Title, newBook.Author, newBook.Genre, newBook.Description, newBook.PublishDate, newBook.Price, newBook.ISBN),
            Success = true,
            Message = "Book added successfully"
        };
    }

    public async Task<ResultDto<BookResponse>> GetBookById(int id)
    {
        var book = await bookRepository.GetBookById(id);
        if (book == null)
        {
            return new ResultDto<BookResponse>
            {
                Success = false,
                Message = "Book not found"
            };
        }
        return new ResultDto<BookResponse>
        {
            Data = new BookResponse(book.Id, book.Title, book.Author, book.Genre, book.Description, book.PublishDate, book.Price, book.ISBN),
            Success = true,
            Message = "Book found"
        };
    }
}