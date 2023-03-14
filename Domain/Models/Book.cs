namespace Domain.Models;

public enum GenreEnum
{
    Novel,
    Comedy,
    ScienceFiction,
    Fantasy
}

public record Book
{
    public Guid Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public GenreEnum Genre { get; set; }
    public List<Author> Authors { get; set; } = new();
    public string? Description { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ExpireDate { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public List<AuthorBook> AuthorsBooks { get; set; } = new();
}