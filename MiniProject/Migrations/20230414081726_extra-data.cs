using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniProject.Migrations
{
    public partial class extradata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraData",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraData",
                table: "Employees");
        }
    }
}
