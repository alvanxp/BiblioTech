using BiblioTech.Data;
using BiblioTech.Domain.Dto;

namespace BiblioTech.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<List<BookDto>> GetBooks()
    {
        var books = await bookRepository.GetBooks();
        return books.Select(book => new BookDto(book.Title, book.Author, book.Year, book.Genre, book.Description)).ToList();
    }
}