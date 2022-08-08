using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYS_Demo.Migrations
{
    public partial class initialCreate07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectCoursesOfStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    StudentFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectCoursesOfStudents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectCoursesOfStudents");
        }
    }
}
