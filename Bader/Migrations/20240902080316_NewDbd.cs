using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bader.Migrations
{
    public partial class NewDbd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "tblMajors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblMajors_CollegeId",
                table: "tblMajors",
                column: "CollegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblMajors_tblColleges_CollegeId",
                table: "tblMajors",
                column: "CollegeId",
                principalTable: "tblColleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblMajors_tblColleges_CollegeId",
                table: "tblMajors");

            migrationBuilder.DropIndex(
                name: "IX_tblMajors_CollegeId",
                table: "tblMajors");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "tblMajors");
        }
    }
}
