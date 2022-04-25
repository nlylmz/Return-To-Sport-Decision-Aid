using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class weşght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "AthleteCriteria",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AthleteCriteria");
        }
    }
}
