using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProGuessApplication.Migrations
{
    public partial class inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "deletado",
                table: "usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "desativado",
                table: "usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deletado",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "desativado",
                table: "usuario");
        }
    }
}
