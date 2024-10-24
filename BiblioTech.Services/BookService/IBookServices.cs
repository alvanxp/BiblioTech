using BiblioTech.Domain.Dto;

namespace BiblioTech.Services.BookService;

public interface IBookService
{
   Task<ResultDto<List<BookResponse>>> GetBooks();
   Task<ResultDto<BookResponse>> AddBook(BookRequest? book);
   Task<ResultDto<BookResponse>> GetBookById(int id);
}