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
}