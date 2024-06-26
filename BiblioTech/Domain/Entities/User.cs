namespace BiblioTech.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public string LastName { get; set; }
    public required string Username { get; set; }
    public string HashedPassword { get; set; }
    public string Salt { get; set; }
    
}