using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.GamenightDB
{
    /// <inheritdoc />
    public partial class Dbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_AspNetUsers_IdentityUserId",
                table: "Persons");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_IdentityUser_IdentityUserId",
                table: "Persons",
                column: "IdentityUserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_IdentityUser_IdentityUserId",
                table: "Persons");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_AspNetUsers_IdentityUserId",
                table: "Persons",
                column: "IdentityUserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
