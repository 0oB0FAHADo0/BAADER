using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bader.Migrations
{
    public partial class attend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingNum",
                table: "tblColleges");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuildingNum",
                table: "tblColleges",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
