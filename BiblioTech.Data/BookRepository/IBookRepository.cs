using BiblioTech.Domain.Entities;

namespace BiblioTech.Data;

public interface IBookRepository
{
    Task<List<Book>?> GetBooks();
    Task<bool?> AddBook(Book book);
    Task<Book?> GetBookById(int id);
}