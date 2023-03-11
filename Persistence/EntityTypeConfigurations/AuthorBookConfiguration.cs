using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations;

public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
{
    public void Configure(EntityTypeBuilder<AuthorBook> builder)
    {
    }
}