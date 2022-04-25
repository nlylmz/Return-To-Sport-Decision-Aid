using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class criteriaValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriteriaValue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteId = table.Column<long>(nullable: false),
                    Criteria1Id = table.Column<long>(nullable: false),
                    Criteria2Id = table.Column<long>(nullable: false),
                    Value = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriteriaValue_Patients_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaValue_AthleteId",
                table: "CriteriaValue",
                column: "AthleteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriteriaValue");
        }
    }
}
