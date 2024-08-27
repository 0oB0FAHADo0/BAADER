using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bader.Migrations
{
    public partial class addingGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "tblSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "tblColleges",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "tblSessions");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "tblColleges");
        }
    }
}
