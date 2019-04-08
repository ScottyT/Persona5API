using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persona5API.Data.Migrations
{
    public partial class CreateDatabases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonaElements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaElements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonaStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Strength = table.Column<int>(nullable: false),
                    Magic = table.Column<int>(nullable: false),
                    Endurance = table.Column<int>(nullable: false),
                    Agility = table.Column<int>(nullable: false),
                    Luck = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillCosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<string>(nullable: true),
                    Resource = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Arcana = table.Column<string>(nullable: true),
                    StatsId = table.Column<int>(nullable: true),
                    SkillsJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_PersonaStats_StatsId",
                        column: x => x.StatsId,
                        principalTable: "PersonaStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelLearned = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(nullable: true),
                    Effect = table.Column<string>(nullable: true),
                    Power = table.Column<int>(nullable: false),
                    CostAmount = table.Column<string>(nullable: true),
                    ElementName = table.Column<string>(nullable: true),
                    CostId = table.Column<int>(nullable: true),
                    ElementId = table.Column<int>(nullable: true),
                    PersonaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonaSkills_SkillCosts_CostId",
                        column: x => x.CostId,
                        principalTable: "SkillCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaSkills_PersonaElements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "PersonaElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaSkills_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personas_StatsId",
                table: "Personas",
                column: "StatsId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaSkills_CostId",
                table: "PersonaSkills",
                column: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaSkills_ElementId",
                table: "PersonaSkills",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaSkills_PersonaId",
                table: "PersonaSkills",
                column: "PersonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonaSkills");

            migrationBuilder.DropTable(
                name: "SkillCosts");

            migrationBuilder.DropTable(
                name: "PersonaElements");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "PersonaStats");
        }
    }
}
