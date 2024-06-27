using BiblioTech.Data;
using BiblioTech.Domain.Dto;
using BiblioTech.Domain.Entities;

namespace BiblioTech.Services.BookService;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<List<BookResponse>> GetBooks()
    {
        //TODO: use a mapper, instead of manual mapping
        var books = await bookRepository.GetBooks();
        return books.Select(book => new BookResponse(book.Id,book.Title, book.Author, book.Genre, book.Description, book.PublishDate, book.Price, book.ISBN)).ToList();
    }

    public async Task<BookResponse> AddBook(BookRequest? book)
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
        await bookRepository.AddBook(newBook);
        return new BookResponse(newBook.Id,newBook.Title, newBook.Author, newBook.Genre, newBook.Description, newBook.PublishDate, newBook.Price, newBook.ISBN);
    }

    public async Task<BookResponse?> GetBookById(int id)
    {
        var book = await bookRepository.GetBookById(id);
        return new BookResponse(book.Id,book.Title, book.Author, book.Genre, book.Description, book.PublishDate, book.Price, book.ISBN);
    }
}