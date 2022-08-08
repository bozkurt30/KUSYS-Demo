using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYS_Demo.Migrations
{
    public partial class initialCreate04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoursetId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoursetId",
                table: "Students");
        }
    }
}
