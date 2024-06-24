using BiblioTech.Domain.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace BiblioTech.Data;

public class BookRepository(IOptions<ConnectionString> connectionString) : IBookRepository
{
    public async Task<List<Book>> GetBooks()
    {
        using var connection = new SqlConnection(connectionString.Value.DefaultConnection);
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT [Id],[Title],[Author],[ISBN],[PublishDate],[Price] FROM Book";
        var reader = await command.ExecuteReaderAsync();
        var books = new List<Book>();
        while (reader.Read())
        {
            books.Add(new Book
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Author = reader.GetString(2),
                ISBN = reader.GetString(3),
                PublishDate = reader.GetDateTime(4),
                Price = reader.GetDecimal(5)
            });
        }

        return books;
    }

    public async Task<bool> AddBook(Book book)
    {
        using var connection = new SqlConnection(connectionString.Value.DefaultConnection);
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Book ([Title],[Author],[Genre],[Description],[PublishDate],[Price],[ISBN]) VALUES (@Title,@Author,@Genre,@Description,@PublishDate,@Price,@ISBN)";
        command.Parameters.AddWithValue("@Title", book.Title);
        command.Parameters.AddWithValue("@Author", book.Author);
        command.Parameters.AddWithValue("@Genre", book.Genre);
        command.Parameters.AddWithValue("@Description", book.Description);
        command.Parameters.AddWithValue("@PublishDate", book.PublishDate);
        command.Parameters.AddWithValue("@Price", book.Price);
        command.Parameters.AddWithValue("@ISBN", book.ISBN);
        var result = await command.ExecuteScalarAsync();
        if (result != null)
        {
            book.Id = Convert.ToInt32(result);    
            return true;
        }

        return false;
    }

    public Task<Book> GetBookById(int id)
    {
        return null;
    }
    
}