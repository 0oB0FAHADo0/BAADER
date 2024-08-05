using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bader.Migrations
{
    public partial class Baader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblContents_tblCourses_CourseId",
                table: "tblContents");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "tblContents");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "tblContents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblContents_tblCourses_CourseId",
                table: "tblContents",
                column: "CourseId",
                principalTable: "tblCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblContents_tblCourses_CourseId",
                table: "tblContents");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "tblContents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "tblContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_tblContents_tblCourses_CourseId",
                table: "tblContents",
                column: "CourseId",
                principalTable: "tblCourses",
                principalColumn: "Id");
        }
    }
}
