namespace BiblioTech.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string HashedPassword { get; set; }
    public required string Salt { get; set; }
    
}