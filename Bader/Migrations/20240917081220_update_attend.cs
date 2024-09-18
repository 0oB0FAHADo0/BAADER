using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bader.Migrations
{
    public partial class update_attend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAttendance_tblSessions_SessionId",
                table: "tblAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_tblContents_tblCourses_CourseId",
                table: "tblContents");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCourses_tblColleges_CollegeId",
                table: "tblCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCourses_tblLevels_LevelId",
                table: "tblCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCourses_tblMajors_MajorId",
                table: "tblCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_tblMajors_tblColleges_CollegeId",
                table: "tblMajors");

            migrationBuilder.DropForeignKey(
                name: "FK_tblPermissions_tblColleges_CollegeId",
                table: "tblPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_tblPermissions_tblRoles_RoleId",
                table: "tblPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_tblRegistrations_tblRegistrationsState_RegistrationStateId",
                table: "tblRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblRegistrations_tblSessions_SessionId",
                table: "tblRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSessions_tblCourses_CourseId",
                table: "tblSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSessions_tblSessionsState_SessionStateId",
                table: "tblSessions");

            migrationBuilder.AddColumn<int>(
                name: "RegistrationId",
                table: "tblAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblAttendance_RegistrationId",
                table: "tblAttendance",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAttendance_tblRegistrations_RegistrationId",
                table: "tblAttendance",
                column: "RegistrationId",
                principalTable: "tblRegistrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAttendance_tblSessions_SessionId",
                table: "tblAttendance",
                column: "SessionId",
                principalTable: "tblSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblContents_tblCourses_CourseId",
                table: "tblContents",
                column: "CourseId",
                principalTable: "tblCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblCourses_tblColleges_CollegeId",
                table: "tblCourses",
                column: "CollegeId",
                principalTable: "tblColleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblCourses_tblLevels_LevelId",
                table: "tblCourses",
                column: "LevelId",
                principalTable: "tblLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblCourses_tblMajors_MajorId",
                table: "tblCourses",
                column: "MajorId",
                principalTable: "tblMajors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblMajors_tblColleges_CollegeId",
                table: "tblMajors",
                column: "CollegeId",
                principalTable: "tblColleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblPermissions_tblColleges_CollegeId",
                table: "tblPermissions",
                column: "CollegeId",
                principalTable: "tblColleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblPermissions_tblRoles_RoleId",
                table: "tblPermissions",
                column: "RoleId",
                principalTable: "tblRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblRegistrations_tblRegistrationsState_RegistrationStateId",
                table: "tblRegistrations",
                column: "RegistrationStateId",
                principalTable: "tblRegistrationsState",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblRegistrations_tblSessions_SessionId",
                table: "tblRegistrations",
                column: "SessionId",
                principalTable: "tblSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSessions_tblCourses_CourseId",
                table: "tblSessions",
                column: "CourseId",
                principalTable: "tblCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSessions_tblSessionsState_SessionStateId",
                table: "tblSessions",
                column: "SessionStateId",
                principalTable: "tblSessionsState",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAttendance_tblRegistrations_RegistrationId",
                table: "tblAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_tblAttendance_tblSessions_SessionId",
                table: "tblAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_tblContents_tblCourses_CourseId",
                table: "tblContents");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCourses_tblColleges_CollegeId",
                table: "tblCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCourses_tblLevels_LevelId",
                table: "tblCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCourses_tblMajors_MajorId",
                table: "tblCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_tblMajors_tblColleges_CollegeId",
                table: "tblMajors");

            migrationBuilder.DropForeignKey(
                name: "FK_tblPermissions_tblColleges_CollegeId",
                table: "tblPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_tblPermissions_tblRoles_RoleId",
                table: "tblPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_tblRegistrations_tblRegistrationsState_RegistrationStateId",
                table: "tblRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblRegistrations_tblSessions_SessionId",
                table: "tblRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSessions_tblCourses_CourseId",
                table: "tblSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSessions_tblSessionsState_SessionStateId",
                table: "tblSessions");

            migrationBuilder.DropIndex(
                name: "IX_tblAttendance_RegistrationId",
                table: "tblAttendance");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "tblAttendance");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAttendance_tblSessions_SessionId",
                table: "tblAttendance",
                column: "SessionId",
                principalTable: "tblSessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblContents_tblCourses_CourseId",
                table: "tblContents",
                column: "CourseId",
                principalTable: "tblCourses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCourses_tblColleges_CollegeId",
                table: "tblCourses",
                column: "CollegeId",
                principalTable: "tblColleges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCourses_tblLevels_LevelId",
                table: "tblCourses",
                column: "LevelId",
                principalTable: "tblLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCourses_tblMajors_MajorId",
                table: "tblCourses",
                column: "MajorId",
                principalTable: "tblMajors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblMajors_tblColleges_CollegeId",
                table: "tblMajors",
                column: "CollegeId",
                principalTable: "tblColleges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPermissions_tblColleges_CollegeId",
                table: "tblPermissions",
                column: "CollegeId",
                principalTable: "tblColleges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPermissions_tblRoles_RoleId",
                table: "tblPermissions",
                column: "RoleId",
                principalTable: "tblRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblRegistrations_tblRegistrationsState_RegistrationStateId",
                table: "tblRegistrations",
                column: "RegistrationStateId",
                principalTable: "tblRegistrationsState",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblRegistrations_tblSessions_SessionId",
                table: "tblRegistrations",
                column: "SessionId",
                principalTable: "tblSessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSessions_tblCourses_CourseId",
                table: "tblSessions",
                column: "CourseId",
                principalTable: "tblCourses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSessions_tblSessionsState_SessionStateId",
                table: "tblSessions",
                column: "SessionStateId",
                principalTable: "tblSessionsState",
                principalColumn: "Id");
        }
    }
}
