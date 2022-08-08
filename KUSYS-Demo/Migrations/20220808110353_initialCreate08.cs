using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYS_Demo.Migrations
{
    public partial class initialCreate08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentFullName",
                table: "SelectCoursesOfStudents",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "SelectCoursesOfStudents",
                newName: "StudentFullName");
        }
    }
}
