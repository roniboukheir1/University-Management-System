using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary1University_Management_System.Infrastructure.Migrations
{
    public partial class AddStudentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add Student-specific properties to Users table
            migrationBuilder.AddColumn<double>(
                name: "AverageGrade",
                table: "Users",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanApplyToFrance",
                table: "Users",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove Student-specific properties from Users table
            migrationBuilder.DropColumn(
                name: "AverageGrade",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CanApplyToFrance",
                table: "Users");
        }
    }
}