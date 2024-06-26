namespace BiblioTech.Domain.Entities;

public class Book
{
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public DateTime PublishDate { get; set; }
    public decimal Price { get; set; }
}