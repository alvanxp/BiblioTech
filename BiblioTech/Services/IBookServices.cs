using BiblioTech.Domain.Dto;

namespace BiblioTech.Services;

public interface IBookService
{
   Task<List<BookDto>> GetBooks();
}