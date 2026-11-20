using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Migrations
{
    public partial class AddGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Animation" },
                    { 4, "Comedy" },
                    { 5, "Crime" },
                    { 6, "Drama" },
                    { 7, "Fantasy" },
                    { 8, "Horror" },
                    { 9, "Romance" },
                    { 10, "Science Fiction" },
                    { 11, "Thriller" }
                });

            migrationBuilder.Sql("""
                SET IDENTITY_INSERT [Genre] ON;
                INSERT INTO [Genre] ([Id], [Name])
                SELECT DISTINCT m.[GenreId], CONCAT(N'Genre ', m.[GenreId])
                FROM [Movie] AS m
                WHERE NOT EXISTS (SELECT 1 FROM [Genre] AS g WHERE g.[Id] = m.[GenreId]);
                SET IDENTITY_INSERT [Genre] OFF;
                """);

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Genre_GenreId",
                table: "Movie",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Genre_GenreId",
                table: "Movie");

            migrationBuilder.DropTable(name: "Genre");
        }
    }
}
