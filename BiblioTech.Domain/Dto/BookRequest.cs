namespace BiblioTech.Domain.Dto;

public record BookRequest(
    int? Id,
    string Title,
    string Author,
    string Genre,
    string Description,
    DateTime PublishDate,
    decimal Price,
    string ISBN);
