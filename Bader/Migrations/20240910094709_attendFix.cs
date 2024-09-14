using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bader.Migrations
{
    public partial class attendFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAttend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblAttendance_tblSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "tblSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAttendance_SessionId",
                table: "tblAttendance",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAttendance");
        }
    }
}
