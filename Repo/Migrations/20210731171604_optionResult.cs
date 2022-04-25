using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class optionResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionResult",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<long>(nullable: false),
                    AthleteId = table.Column<long>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionResult_Patients_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OptionResult_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionResult_AthleteId",
                table: "OptionResult",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionResult_OptionId",
                table: "OptionResult",
                column: "OptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionResult");
        }
    }
}
