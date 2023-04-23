using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(author => author.Id);
        
        builder.HasMany(a => a.Books)
            .WithMany(a => a.Authors)
            .UsingEntity<AuthorBook>(book => book
                    .HasOne(ab => ab.Book)
                    .WithMany(b => b.AuthorsBooks)
                    .HasForeignKey(ab => ab.BookId),
                author => author
                    .HasOne(ab => ab.Author)
                    .WithMany(a => a.AuthorsBooks)
                    .HasForeignKey(ab => ab.AuthorId)
            );
    }
}