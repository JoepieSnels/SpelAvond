using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.GamenightDB
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameNights",
                columns: table => new
                {
                    gameNightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hostId = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    maxPlayers = table.Column<int>(type: "int", nullable: false),
                    food = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lactoseFree = table.Column<bool>(type: "bit", nullable: false),
                    alcoholic = table.Column<bool>(type: "bit", nullable: false),
                    nutFree = table.Column<bool>(type: "bit", nullable: false),
                    vegetarian = table.Column<bool>(type: "bit", nullable: false),
                    is18Plus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNights", x => x.gameNightId);
                });

            migrationBuilder.CreateTable(
                name: "GamesDb",
                columns: table => new
                {
                    gameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genre = table.Column<int>(type: "int", nullable: false),
                    is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeOfGame = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesDb", x => x.gameId);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamenightGames",
                columns: table => new
                {
                    gameId = table.Column<int>(type: "int", nullable: false),
                    gameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamenightGames", x => new { x.gameId, x.gameNightId });
                    table.ForeignKey(
                        name: "FK_GamenightGames_GameNights_gameNightId",
                        column: x => x.gameNightId,
                        principalTable: "GameNights",
                        principalColumn: "gameNightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamenightGames_GamesDb_gameId",
                        column: x => x.gameId,
                        principalTable: "GamesDb",
                        principalColumn: "gameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    personId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    houseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    lactoseFree = table.Column<bool>(type: "bit", nullable: false),
                    alcoholic = table.Column<bool>(type: "bit", nullable: false),
                    nutFree = table.Column<bool>(type: "bit", nullable: false),
                    vegatarian = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.personId);
                    table.ForeignKey(
                        name: "FK_Persons_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamenightPlayers",
                columns: table => new
                {
                    personId = table.Column<int>(type: "int", nullable: false),
                    gameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamenightPlayers", x => new { x.personId, x.gameNightId });
                    table.ForeignKey(
                        name: "FK_GamenightPlayers_GameNights_gameNightId",
                        column: x => x.gameNightId,
                        principalTable: "GameNights",
                        principalColumn: "gameNightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamenightPlayers_Persons_personId",
                        column: x => x.personId,
                        principalTable: "Persons",
                        principalColumn: "personId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reviewerId = table.Column<int>(type: "int", nullable: false),
                    gameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_GameNights_gameNightId",
                        column: x => x.gameNightId,
                        principalTable: "GameNights",
                        principalColumn: "gameNightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Persons_reviewerId",
                        column: x => x.reviewerId,
                        principalTable: "Persons",
                        principalColumn: "personId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamenightGames_gameNightId",
                table: "GamenightGames",
                column: "gameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_GamenightPlayers_gameNightId",
                table: "GamenightPlayers",
                column: "gameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_IdentityUserId",
                table: "Persons",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_gameNightId",
                table: "Reviews",
                column: "gameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_reviewerId",
                table: "Reviews",
                column: "reviewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamenightGames");

            migrationBuilder.DropTable(
                name: "GamenightPlayers");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "GamesDb");

            migrationBuilder.DropTable(
                name: "GameNights");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
