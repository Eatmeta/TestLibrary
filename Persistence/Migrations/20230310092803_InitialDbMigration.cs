using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Isbn = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BookId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"), new DateOnly(1960, 11, 10), null, "Neil", "Gaiman" },
                    { new Guid("6fff3331-3bdd-4ca2-a178-6a35fd653c59"), new DateOnly(1948, 4, 28), null, "Terry", "Pratchett" },
                    { new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), new DateOnly(1799, 6, 6), null, "Alexander", "Pushkin" },
                    { new Guid("a1784ca2-887b-4132-3bdd-9935fd65dd55"), new DateOnly(1974, 7, 14), null, "David", "Mitchell" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "Isbn", "IssueDate", "ReturnDate", "Title" },
                values: new object[,]
                {
                    { new Guid("150c81c6-2458-466e-907a-2df11325e2b8"), "A postmodern visionary who is also a master of styles of genres, David Mitchell combines flat-out adventure, a Nabokovian lore of puzzles, a keen eye for character, and a taste for mind-bending philosophical and scientific speculation.", "9781529324983", null, null, "Cloud Atlas" },
                    { new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"), "Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general.", "9783161484100", null, null, "Eugene Onegin" },
                    { new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"), "Internationally bestselling authors Neil Gaiman and Terry Pratchett teamed up to write this witty comedy about the birth Satan’s son and the coming of the End Times.", "9780552137034", null, null, "Good Omens" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "BookId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Novel" },
                    { 2, null, "Comedy" },
                    { 3, null, "Science Fiction" },
                    { 4, null, "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBooks",
                columns: new[] { "Id", "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), new Guid("98474b8e-d713-401e-8aee-acb7353f97bb") },
                    { 2, new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"), new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba") },
                    { 3, new Guid("6fff3331-3bdd-4ca2-a178-6a35fd653c59"), new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba") },
                    { 4, new Guid("a1784ca2-887b-4132-3bdd-9935fd65dd55"), new Guid("150c81c6-2458-466e-907a-2df11325e2b8") }
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "Id", "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"), 1 },
                    { 2, new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"), 2 },
                    { 3, new Guid("150c81c6-2458-466e-907a-2df11325e2b8"), 3 },
                    { 4, new Guid("150c81c6-2458-466e-907a-2df11325e2b8"), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_AuthorId",
                table: "AuthorBooks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_BookId",
                table: "AuthorBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_Id",
                table: "AuthorBooks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookId",
                table: "Authors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Id",
                table: "Authors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenreId",
                table: "BookGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_Id",
                table: "BookGenres",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Id",
                table: "Books",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BookId",
                table: "Genres",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Id",
                table: "Genres",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
