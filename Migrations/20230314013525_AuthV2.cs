using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProGuessApplication.Migrations
{
    public partial class AuthV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeepLoggedIn",
                table: "usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "KeepLoggedIn",
                table: "usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
