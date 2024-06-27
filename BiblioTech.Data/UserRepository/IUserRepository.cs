using BiblioTech.Domain.Entities;

namespace BiblioTech.Data;

public interface IUserRepository
{
    Task<User?> Insert(User user);
    Task<User?> GetUserByUsername(string modelUsername);
}