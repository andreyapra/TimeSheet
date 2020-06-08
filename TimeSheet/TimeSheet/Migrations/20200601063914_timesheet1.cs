using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeSheet.Migrations
{
    public partial class timesheet1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblProjects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProjects", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "TblTimeSheetEntry",
                columns: table => new
                {
                    TimesheetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    WorkHour = table.Column<int>(nullable: false),
                    Keterangan = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ManagerID = table.Column<string>(nullable: true),
                    TblProjectsProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTimeSheetEntry", x => x.TimesheetID);
                    table.ForeignKey(
                        name: "FK_TblTimeSheetEntry_TblProjects_TblProjectsProjectID",
                        column: x => x.TblProjectsProjectID,
                        principalTable: "TblProjects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblTimeSheetEntry_TblProjectsProjectID",
                table: "TblTimeSheetEntry",
                column: "TblProjectsProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblTimeSheetEntry");

            migrationBuilder.DropTable(
                name: "TblProjects");
        }
    }
}
