using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameService.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    PlatformID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.PlatformID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    gameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    PlatformID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.gameID);
                    table.ForeignKey(
                        name: "FK_Games_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Platforms_PlatformID",
                        column: x => x.PlatformID,
                        principalTable: "Platforms",
                        principalColumn: "PlatformID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreID", "GenreName" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "RPG" },
                    { 4, "Simulation" },
                    { 5, "Strategy" },
                    { 6, "Sports" },
                    { 7, "Puzzle" },
                    { 8, "Idle" },
                    { 9, "Casual" }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "PlatformID", "Name" },
                values: new object[,]
                {
                    { 1, "PS5" },
                    { 2, "Xbox" },
                    { 3, "PC" },
                    { 4, "Android" },
                    { 5, "iOS" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "gameID", "Description", "GenreID", "PlatformID", "Price", "Publisher", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, "A game about heroes in action", 1, 2, 10.99m, "Petroglyph", 2005, "Heroes in Action" },
                    { 2, "A game about adventure in the forest", 2, 3, 9.99m, "Inc Mania", 2012, "Adventures in the Black Forest" },
                    { 3, "An RPG game in the city", 3, 4, 8.99m, "Kronos Studios", 2021, "Escape the City" },
                    { 4, "A game about heroes in action", 1, 1, 10.99m, "Petroglyph", 2005, "Heroes in Action" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GenreID",
                table: "Games",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlatformID",
                table: "Games",
                column: "PlatformID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Platforms");
        }
    }
}
