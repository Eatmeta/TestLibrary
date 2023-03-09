﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230309111649_InitialIdentityApplicationDbMigration")]
    partial class InitialIdentityApplicationDbMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AuthorId"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AuthorId");

                    b.HasIndex("AuthorId")
                        .IsUnique();

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            BirthDate = new DateOnly(1799, 6, 6),
                            FirstName = "Alexander",
                            LastName = "Pushkin"
                        },
                        new
                        {
                            AuthorId = 2,
                            BirthDate = new DateOnly(1960, 11, 10),
                            FirstName = "Neil",
                            LastName = "Gaiman"
                        },
                        new
                        {
                            AuthorId = 3,
                            BirthDate = new DateOnly(1948, 4, 28),
                            FirstName = "Terry",
                            LastName = "Pratchett"
                        },
                        new
                        {
                            AuthorId = 4,
                            BirthDate = new DateOnly(1974, 7, 14),
                            FirstName = "David",
                            LastName = "Mitchell"
                        });
                });

            modelBuilder.Entity("Domain.Models.AuthorBook", b =>
                {
                    b.Property<int>("AuthorBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AuthorBookId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.HasKey("AuthorBookId");

                    b.HasIndex("AuthorBookId")
                        .IsUnique();

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBooks");

                    b.HasData(
                        new
                        {
                            AuthorBookId = 1,
                            AuthorId = 1,
                            BookId = 1
                        },
                        new
                        {
                            AuthorBookId = 2,
                            AuthorId = 2,
                            BookId = 2
                        },
                        new
                        {
                            AuthorBookId = 3,
                            AuthorId = 3,
                            BookId = 2
                        },
                        new
                        {
                            AuthorBookId = 4,
                            AuthorId = 4,
                            BookId = 3
                        });
                });

            modelBuilder.Entity("Domain.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly?>("IssueDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("ReturnDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("BookId");

                    b.HasIndex("BookId")
                        .IsUnique();

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Description = "Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general.",
                            Isbn = "9783161484100",
                            Title = "Eugene Onegin"
                        },
                        new
                        {
                            BookId = 2,
                            Description = "Internationally bestselling authors Neil Gaiman and Terry Pratchett teamed up to write this witty comedy about the birth Satan’s son and the coming of the End Times.",
                            Isbn = "9780552137034",
                            Title = "Good Omens"
                        },
                        new
                        {
                            BookId = 3,
                            Description = "A postmodern visionary who is also a master of styles of genres, David Mitchell combines flat-out adventure, a Nabokovian lore of puzzles, a keen eye for character, and a taste for mind-bending philosophical and scientific speculation.",
                            Isbn = "9781529324983",
                            Title = "Cloud Atlas"
                        });
                });

            modelBuilder.Entity("Domain.Models.BookGenre", b =>
                {
                    b.Property<int>("BookGenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookGenreId"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.HasKey("BookGenreId");

                    b.HasIndex("BookGenreId")
                        .IsUnique();

                    b.HasIndex("BookId");

                    b.HasIndex("GenreId");

                    b.ToTable("BookGenres");

                    b.HasData(
                        new
                        {
                            BookGenreId = 1,
                            BookId = 1,
                            GenreId = 1
                        },
                        new
                        {
                            BookGenreId = 2,
                            BookId = 2,
                            GenreId = 2
                        },
                        new
                        {
                            BookGenreId = 3,
                            BookId = 3,
                            GenreId = 3
                        },
                        new
                        {
                            BookGenreId = 4,
                            BookId = 3,
                            GenreId = 4
                        });
                });

            modelBuilder.Entity("Domain.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GenreId");

                    b.HasIndex("GenreId")
                        .IsUnique();

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Name = "Novel"
                        },
                        new
                        {
                            GenreId = 2,
                            Name = "Comedy"
                        },
                        new
                        {
                            GenreId = 3,
                            Name = "Science Fiction"
                        },
                        new
                        {
                            GenreId = 4,
                            Name = "Fantasy"
                        });
                });

            modelBuilder.Entity("Domain.Models.AuthorBook", b =>
                {
                    b.HasOne("Domain.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Domain.Models.BookGenre", b =>
                {
                    b.HasOne("Domain.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Genre");
                });
#pragma warning restore 612, 618
        }
    }
}
