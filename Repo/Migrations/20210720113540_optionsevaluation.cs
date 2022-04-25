using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class optionsevaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionsEvaluation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteId = table.Column<long>(nullable: false),
                    CriteriaId = table.Column<long>(nullable: false),
                    Option1Id = table.Column<long>(nullable: false),
                    Option2Id = table.Column<long>(nullable: false),
                    Value = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsEvaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionsEvaluation_Patients_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionsEvaluation_AthleteId",
                table: "OptionsEvaluation",
                column: "AthleteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionsEvaluation");
        }
    }
}
