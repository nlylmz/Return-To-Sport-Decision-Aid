using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class athletecriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AthleteCriteria",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaId = table.Column<long>(nullable: false),
                    AthleteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AthleteCriteria_Patients_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AthleteCriteria_Criteria_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthleteCriteria_AthleteId",
                table: "AthleteCriteria",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteCriteria_CriteriaId",
                table: "AthleteCriteria",
                column: "CriteriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthleteCriteria");
        }
    }
}
