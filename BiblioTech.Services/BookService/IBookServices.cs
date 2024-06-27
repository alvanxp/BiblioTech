using BiblioTech.Domain.Dto;

namespace BiblioTech.Services.BookService;

public interface IBookService
{
   Task<List<BookResponse>> GetBooks();
   Task<BookResponse> AddBook(BookRequest? book);
   Task<BookResponse?> GetBookById(int id);
}