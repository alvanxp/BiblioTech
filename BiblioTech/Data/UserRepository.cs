using BiblioTech.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace BiblioTech.Data;

public class UserRepository(IOptions<ConnectionString> connectionString) : IUserRepository
{

    public async Task<User?> Insert(User user)
    {
        await using var connection = new SqlConnection(connectionString.Value.DefaultConnection);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();

        command.CommandText =
            "INSERT INTO [User] (FirstName, LastName, Username, Salt, HashedPassword) VALUES (@FirstName, @LastName, @Username, @Salt, @HashedPassword)";
        command.Parameters.AddWithValue("@FirstName", user.FirstName);
        command.Parameters.AddWithValue("@LastName", user.LastName);
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Salt", user.Salt);
        command.Parameters.AddWithValue("@HashedPassword", user.HashedPassword);

        var id = await command.ExecuteScalarAsync();
        user.Id = Convert.ToInt32(id);
        return user;
    }

    public async Task<User> Update(User user)
    {
        await using var connection = new SqlConnection(connectionString.Value.DefaultConnection);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();

        command.CommandText =
            "UPDATE [User] SET FirstName = @FirstName, LastName = @LastName, Username = @Username, Salt = @Salt, HashedPassword = @HashedPassword WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", user.Id);
        command.Parameters.AddWithValue("@FirstName", user.FirstName);
        command.Parameters.AddWithValue("@LastName", user.LastName);
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Salt", user.Salt);
        command.Parameters.AddWithValue("@HashedPassword", user.HashedPassword);

        await command.ExecuteNonQueryAsync();
        return user;
    }

    public async Task<User?> GetUserByUsername(string modelUsername)
    {
        //access to user table using ado.net
        await using var connection = new SqlConnection(connectionString.Value.DefaultConnection);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();

        command.CommandText =
            "SELECT Id, FirstName, LastName, Username, HashedPassword, Salt FROM [User] WHERE Username = @Username";
        command.Parameters.AddWithValue("@Username", modelUsername);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return new User
        {
            Id = reader.GetInt32(0),
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            Username = reader.GetString(3),
            HashedPassword = reader.GetString(4),
            Salt = reader.GetString(5)
        };
    }
}