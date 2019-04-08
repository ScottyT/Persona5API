using Microsoft.EntityFrameworkCore.Migrations;

namespace Persona5API.Data.Migrations
{
    public partial class FieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LevelLearned",
                table: "Personas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelLearned",
                table: "Personas");
        }
    }
}
