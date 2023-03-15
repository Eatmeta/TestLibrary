using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.EntityTypeConfigurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(book => book.Id);

        builder.HasIndex(u => u.Isbn).IsUnique();
        builder.Property(book => book.Isbn).HasMaxLength(13);
        builder.Property(book => book.Title).HasMaxLength(500);

        builder.Property(book => book.Genre).HasConversion(new EnumToStringConverter<GenreEnum>());

        builder.HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<AuthorBook>(author => author
                    .HasOne(ab => ab.Author)
                    .WithMany(a => a.AuthorsBooks)
                    .HasForeignKey(ab => ab.AuthorId),
                book => book
                    .HasOne(ab => ab.Book)
                    .WithMany(b => b.AuthorsBooks)
                    .HasForeignKey(ab => ab.BookId)
            );
    }
}