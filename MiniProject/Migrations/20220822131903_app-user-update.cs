using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniProject.Migrations
{
    public partial class appuserupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AppUser");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "AppUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_EmployeeId",
                table: "AppUser",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Employees_EmployeeId",
                table: "AppUser",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_Employees_EmployeeId",
                table: "AppUser");

            migrationBuilder.DropIndex(
                name: "IX_AppUser_EmployeeId",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AppUser");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
