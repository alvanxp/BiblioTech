namespace BiblioTech.Domain.Dto;

public record BookResponse(int? Id, string Title, string Author,  string Genre, string Description, DateTime PublishDate, decimal Price, string ISBN);