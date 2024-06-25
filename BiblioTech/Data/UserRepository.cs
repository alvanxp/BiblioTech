using BiblioTech.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace BiblioTech.Data;

public class UserRepository(IOptions<ConnectionString> connectionString) : IUserRepository
{

    public async Task<User?> Insert(User userObj)
    {
        using var connection = new SqlConnection(connectionString.Value.DefaultConnection);
        await connection.OpenAsync();
        using var command = connection.CreateCommand();
        
        command.CommandText = "INSERT INTO [User] (FirstName, LastName, Username, Salt, HashedPassword) VALUES (@FirstName, @LastName, @Username, @Salt, @HashedPassword)";
        command.Parameters.AddWithValue("@FirstName", userObj.FirstName);
        command.Parameters.AddWithValue("@LastName", userObj.LastName);
        command.Parameters.AddWithValue("@Username", userObj.Username);
        command.Parameters.AddWithValue("@Salt", userObj.Salt);
        command.Parameters.AddWithValue("@HashedPassword", userObj.HashedPassword);

        var id =await command.ExecuteScalarAsync();
        userObj.Id = Convert.ToInt32(id);
        return userObj;
    }
    
   public async Task<User> Update(User userObj)
    {
        using var connection = new SqlConnection(connectionString.Value.DefaultConnection);
        await connection.OpenAsync();
        using var command = connection.CreateCommand();
        
        command.CommandText = "UPDATE [User] SET FirstName = @FirstName, LastName = @LastName, Username = @Username, Salt = @Salt, HashedPassword = @HashedPassword WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", userObj.Id);
        command.Parameters.AddWithValue("@FirstName", userObj.FirstName);
        command.Parameters.AddWithValue("@LastName", userObj.LastName);
        command.Parameters.AddWithValue("@Username", userObj.Username);
        command.Parameters.AddWithValue("@Salt", userObj.Salt);
        command.Parameters.AddWithValue("@HashedPassword", userObj.HashedPassword);

        await command.ExecuteNonQueryAsync();
        return userObj;
    } 

    public async Task<User> GetUserByUsername(string modelUsername)
    {
       //access to user table using ado.net
        using var connection = new SqlConnection(connectionString.Value.DefaultConnection);
        await connection.OpenAsync();
        using var command = connection.CreateCommand();
        
        command.CommandText = "SELECT * FROM [User] WHERE Username = @Username";
        command.Parameters.AddWithValue("@Username", modelUsername);
        
        using var reader = await command.ExecuteReaderAsync();
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