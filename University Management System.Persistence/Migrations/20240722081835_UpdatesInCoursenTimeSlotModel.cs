using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University_Management_System.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesInCoursenTimeSlotModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Course_ClassId",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "TimeSlots",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlots_ClassId",
                table: "TimeSlots",
                newName: "IX_TimeSlots_CourseId");

            migrationBuilder.AlterColumn<int>(
                name: "DayPattern",
                table: "TimeSlots",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Course_CourseId",
                table: "TimeSlots",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Course_CourseId",
                table: "TimeSlots");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "TimeSlots",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlots_CourseId",
                table: "TimeSlots",
                newName: "IX_TimeSlots_ClassId");

            migrationBuilder.AlterColumn<string>(
                name: "DayPattern",
                table: "TimeSlots",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Course",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Course_ClassId",
                table: "TimeSlots",
                column: "ClassId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
