using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bader.Migrations
{
    public partial class AddDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblColleges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblColleges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblContentsLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContentsLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCoursesLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCoursesLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelNum = table.Column<int>(type: "int", nullable: false),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPermissionsLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    OperationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Createdto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPermissionsLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRegistrationsLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationState = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRegistrationsLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRegistrationsState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRegistrationsState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSessionsLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSessionsLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSessionsState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSessionsState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Usertype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    CourseNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCourses_tblColleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "tblColleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCourses_tblLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "tblLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CollegeId = table.Column<int>(type: "int", nullable: false),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblColleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "tblColleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tblRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentsAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentsEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Links = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblContents_tblCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tblCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionStateId = table.Column<int>(type: "int", nullable: false),
                    SessionNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Links = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfStudents = table.Column<int>(type: "int", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSessions_tblCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tblCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSessions_tblSessionsState_SessionStateId",
                        column: x => x.SessionStateId,
                        principalTable: "tblSessionsState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationStateId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblRegistrations_tblRegistrationsState_RegistrationStateId",
                        column: x => x.RegistrationStateId,
                        principalTable: "tblRegistrationsState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRegistrations_tblSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "tblSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblContents_CourseId",
                table: "tblContents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCourses_CollegeId",
                table: "tblCourses",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCourses_LevelId",
                table: "tblCourses",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_CollegeId",
                table: "tblPermissions",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_RoleId",
                table: "tblPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRegistrations_RegistrationStateId",
                table: "tblRegistrations",
                column: "RegistrationStateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRegistrations_SessionId",
                table: "tblRegistrations",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSessions_CourseId",
                table: "tblSessions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSessions_SessionStateId",
                table: "tblSessions",
                column: "SessionStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblContents");

            migrationBuilder.DropTable(
                name: "tblContentsLogs");

            migrationBuilder.DropTable(
                name: "tblCoursesLogs");

            migrationBuilder.DropTable(
                name: "tblPermissions");

            migrationBuilder.DropTable(
                name: "tblPermissionsLogs");

            migrationBuilder.DropTable(
                name: "tblRegistrations");

            migrationBuilder.DropTable(
                name: "tblRegistrationsLogs");

            migrationBuilder.DropTable(
                name: "tblSessionsLogs");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblRoles");

            migrationBuilder.DropTable(
                name: "tblRegistrationsState");

            migrationBuilder.DropTable(
                name: "tblSessions");

            migrationBuilder.DropTable(
                name: "tblCourses");

            migrationBuilder.DropTable(
                name: "tblSessionsState");

            migrationBuilder.DropTable(
                name: "tblColleges");

            migrationBuilder.DropTable(
                name: "tblLevels");
        }
    }
}
