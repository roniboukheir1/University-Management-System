using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University_Management_System.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGradesAndProfilePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "TimeSlots",
                type: "text",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<string>(
                name: "EndTime",
                table: "TimeSlots",
                type: "text",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AddColumn<double>(
                name: "AverageGrade",
                table: "Students",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "CanApplyToFrance",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "Enrollments",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageGrade",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CanApplyToFrance",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Enrollments");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "TimeSlots",
                type: "interval",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "TimeSlots",
                type: "interval",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
