namespace Domain.Models;

public class Author
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public List<Book> Books { get; set; } = new();
    [System.Text.Json.Serialization.JsonIgnore]
    public List<AuthorBook> AuthorsBooks { get; set; } = new();
    
    public override string ToString() => $"{Id}:{FirstName}:{LastName}";
}