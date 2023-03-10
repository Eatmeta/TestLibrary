using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class AuthorBook
{
    public int Id { get; set; }
    public Guid AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public virtual Author Author { get; set; }
    public Guid BookId { get; set; }
    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }
}