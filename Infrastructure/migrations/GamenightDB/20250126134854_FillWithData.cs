using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.GamenightDB
{
    /// <inheritdoc />
    public partial class FillWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "identityUserId",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "GameNights",
                columns: new[] { "gameNightId", "address", "alcoholic", "dateTime", "food", "hostId", "is18Plus", "lactoseFree", "maxPlayers", "nutFree", "vegetarian" },
                values: new object[,]
                {
                    { 1, "Breda", true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No food", 1, true, true, 4, true, true },
                    { 2, "Tilburg", true, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "No food", 1, true, true, 19, true, true },
                    { 3, "Rotterdam", true, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "No food", 2, false, true, 16, true, true },
                    { 4, "Roosendaal", true, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "No food", 2, false, true, 25, true, true }
                });

            migrationBuilder.InsertData(
                table: "GamesDb",
                columns: new[] { "gameId", "description", "genre", "image", "is18Plus", "name", "typeOfGame" },
                values: new object[,]
                {
                    { 1, "Description1", 4, "Image1", true, "Game1", 0 },
                    { 2, "Description2", 4, "Image2", true, "Game2", 2 },
                    { 3, "Description3", 4, "Image3", true, "Game3", 1 },
                    { 4, "Description4", 4, "Image4", true, "Game4", 0 }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "personId", "alcoholic", "birthDate", "city", "email", "firstName", "gender", "houseNumber", "lactoseFree", "lastName", "nutFree", "street", "vegatarian" },
                values: new object[,]
                {
                    { 2, true, new DateTime(1992, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "City2", "jane.doe@email.com", "Jane", 1, "2", true, "Doe", false, "Street2", false },
                    { 3, true, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "City1", "john.doe@email.com", "John", 0, "1", true, "Doe", false, "Street1", false }
                });

            migrationBuilder.InsertData(
                table: "GamenightGames",
                columns: new[] { "gameId", "gameNightId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "GamenightPlayers",
                columns: new[] { "gameNightId", "personId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "reviewId", "comment", "gameNightId", "rating", "reviewerId" },
                values: new object[,]
                {
                    { 1, "Great game night!", 1, 5, 3 },
                    { 2, "Enjoyed the evening!", 2, 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_email",
                table: "Persons",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_email",
                table: "Persons");

            migrationBuilder.DeleteData(
                table: "GameNights",
                keyColumn: "gameNightId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GameNights",
                keyColumn: "gameNightId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GamenightGames",
                keyColumns: new[] { "gameId", "gameNightId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GamenightGames",
                keyColumns: new[] { "gameId", "gameNightId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "GamenightGames",
                keyColumns: new[] { "gameId", "gameNightId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "GamenightGames",
                keyColumns: new[] { "gameId", "gameNightId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "GamenightPlayers",
                keyColumns: new[] { "gameNightId", "personId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GamenightPlayers",
                keyColumns: new[] { "gameNightId", "personId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "reviewId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "reviewId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GameNights",
                keyColumn: "gameNightId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GameNights",
                keyColumn: "gameNightId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GamesDb",
                keyColumn: "gameId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GamesDb",
                keyColumn: "gameId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GamesDb",
                keyColumn: "gameId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GamesDb",
                keyColumn: "gameId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "personId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "personId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "identityUserId",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
