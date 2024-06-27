using BiblioTech.Domain.Dto;

namespace BiblioTech.Services.BookService;

public interface IBookService
{
   Task<List<BookDto>> GetBooks();
   Task<ResultDto<bool>> AddBook(BookDto book);
   Task<BookDto?> GetBookById(int id);
}