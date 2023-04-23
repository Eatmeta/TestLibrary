using System.Reflection;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<AuthorBook> AuthorsBooks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        //new DbInitializer(modelBuilder).Seed();
    }
}