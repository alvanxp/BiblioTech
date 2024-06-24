namespace BiblioTech.Data;

public interface IBookRepository
{
    Task<List<Book>> GetBooks();
    
}