using BiblioTech.Domain.Dto;

namespace BiblioTech.Services;

public interface IBookService
{
   Task<List<BookDto>> GetBooks();
   Task<bool?> AddBook(BookDto book);
   Task<BookDto?> GetBookById(int id);
}