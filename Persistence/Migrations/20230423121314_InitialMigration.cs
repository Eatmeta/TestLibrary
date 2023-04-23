using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Isbn = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Genre = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpireDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorsBooks",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsBooks", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorsBooks_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorsBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"), new DateOnly(1960, 11, 10), "Neil", "Gaiman" },
                    { new Guid("6fff3331-3bdd-4ca2-a178-6a35fd653c59"), new DateOnly(1948, 4, 28), "Terry", "Pratchett" },
                    { new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), new DateOnly(1799, 6, 6), "Alexander", "Pushkin" },
                    { new Guid("a1784ca2-887b-4132-3bdd-9935fd65dd55"), new DateOnly(1974, 7, 14), "David", "Mitchell" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "ExpireDate", "Genre", "Isbn", "IssueDate", "Title" },
                values: new object[,]
                {
                    { new Guid("150c81c6-2458-466e-907a-2df11325e2b8"), "A postmodern visionary who is also a master of styles of genres, David Mitchell combines flat-out adventure, a Nabokovian lore of puzzles, a keen eye for character, and a taste for mind-bending philosophical and scientific speculation.", null, "ScienceFiction", "9781529324983", null, "Cloud Atlas" },
                    { new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"), "Eugene Onegin is a novel written in verse, and is one of the most influential works of Pushkin in particular and for Russian literature in general.", null, "Novel", "9783161484100", null, "Eugene Onegin" },
                    { new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"), "Internationally bestselling authors Neil Gaiman and Terry Pratchett teamed up to write this witty comedy about the birth Satan’s son and the coming of the End Times.", null, "Comedy", "9780552137034", null, "Good Omens" }
                });

            migrationBuilder.InsertData(
                table: "AuthorsBooks",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"), new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba") },
                    { new Guid("6fff3331-3bdd-4ca2-a178-6a35fd653c59"), new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba") },
                    { new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), new Guid("98474b8e-d713-401e-8aee-acb7353f97bb") },
                    { new Guid("a1784ca2-887b-4132-3bdd-9935fd65dd55"), new Guid("150c81c6-2458-466e-907a-2df11325e2b8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsBooks_BookId",
                table: "AuthorsBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn",
                table: "Books",
                column: "Isbn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorsBooks");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
