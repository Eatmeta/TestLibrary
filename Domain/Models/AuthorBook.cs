using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class AuthorBook
{
    public int AuthorBookId { get; set; }
    public int AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public virtual Author Author { get; set; }
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }
}