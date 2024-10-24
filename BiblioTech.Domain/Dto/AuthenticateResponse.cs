using BiblioTech.Domain.Entities;

namespace BiblioTech.Domain.Dto;

public class AuthenticateResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Token { get; set; }
}