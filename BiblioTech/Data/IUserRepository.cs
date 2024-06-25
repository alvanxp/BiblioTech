using BiblioTech.Domain.Entities;

namespace BiblioTech.Data;

public interface IUserRepository
{
    Task<User?> Insert(User userObj);
    Task<User?> GetUserByUsername(string modelUsername);
}