using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.GamenightDB
{
    /// <inheritdoc />
    public partial class AddSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Gamenight");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Reviews",
                newSchema: "Gamenight");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Persons",
                newSchema: "Gamenight");

            migrationBuilder.RenameTable(
                name: "GamesDb",
                newName: "GamesDb",
                newSchema: "Gamenight");

            migrationBuilder.RenameTable(
                name: "GameNights",
                newName: "GameNights",
                newSchema: "Gamenight");

            migrationBuilder.RenameTable(
                name: "GamenightPlayers",
                newName: "GamenightPlayers",
                newSchema: "Gamenight");

            migrationBuilder.RenameTable(
                name: "GamenightGames",
                newName: "GamenightGames",
                newSchema: "Gamenight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Reviews",
                schema: "Gamenight",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Persons",
                schema: "Gamenight",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "GamesDb",
                schema: "Gamenight",
                newName: "GamesDb");

            migrationBuilder.RenameTable(
                name: "GameNights",
                schema: "Gamenight",
                newName: "GameNights");

            migrationBuilder.RenameTable(
                name: "GamenightPlayers",
                schema: "Gamenight",
                newName: "GamenightPlayers");

            migrationBuilder.RenameTable(
                name: "GamenightGames",
                schema: "Gamenight",
                newName: "GamenightGames");
        }
    }
}
