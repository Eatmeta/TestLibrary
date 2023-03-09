namespace Domain.Models;

public class Book
{
    public int BookId { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
}