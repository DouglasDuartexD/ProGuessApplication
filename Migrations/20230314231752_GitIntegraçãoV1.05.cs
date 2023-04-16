using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProGuessApplication.Migrations
{
    public partial class GitIntegraçãoV105 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "git");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "git",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git", x => x.id);
                });
        }
    }
}
