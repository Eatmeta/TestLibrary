namespace Domain.Models;

public record AuthorBook
{
    public Guid AuthorId { get; set; }
    public Author Author {  get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }

}