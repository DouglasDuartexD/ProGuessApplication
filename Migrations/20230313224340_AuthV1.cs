using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProGuessApplication.Migrations
{
    public partial class AuthV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "KeepLoggedIn",
                table: "usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeepLoggedIn",
                table: "usuario");
        }
    }
}
