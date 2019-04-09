using Microsoft.EntityFrameworkCore.Migrations;

namespace Persona5API.Data.Migrations
{
    public partial class AddedPersonaFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonaSkills_Personas_PersonaId",
                table: "PersonaSkills");

            migrationBuilder.DropIndex(
                name: "IX_PersonaSkills_PersonaId",
                table: "PersonaSkills");

            migrationBuilder.AddColumn<string>(
                name: "ResistJson",
                table: "Personas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeakJson",
                table: "Personas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResistJson",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "WeakJson",
                table: "Personas");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaSkills_PersonaId",
                table: "PersonaSkills",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaSkills_Personas_PersonaId",
                table: "PersonaSkills",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
