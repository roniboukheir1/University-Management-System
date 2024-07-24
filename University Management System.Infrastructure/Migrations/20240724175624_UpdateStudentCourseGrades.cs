using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary1University_Management_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentCourseGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseGrade_Courses_CourseId",
                table: "StudentCourseGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseGrade_Students_StudentId",
                table: "StudentCourseGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourseGrade",
                table: "StudentCourseGrade");

            migrationBuilder.RenameTable(
                name: "StudentCourseGrade",
                newName: "StudentCourseGrades");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourseGrade_StudentId",
                table: "StudentCourseGrades",
                newName: "IX_StudentCourseGrades_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourseGrade_CourseId",
                table: "StudentCourseGrades",
                newName: "IX_StudentCourseGrades_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourseGrades",
                table: "StudentCourseGrades",
                column: "StudentCourseGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseGrades_Courses_CourseId",
                table: "StudentCourseGrades",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseGrades_Students_StudentId",
                table: "StudentCourseGrades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseGrades_Courses_CourseId",
                table: "StudentCourseGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseGrades_Students_StudentId",
                table: "StudentCourseGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourseGrades",
                table: "StudentCourseGrades");

            migrationBuilder.RenameTable(
                name: "StudentCourseGrades",
                newName: "StudentCourseGrade");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourseGrades_StudentId",
                table: "StudentCourseGrade",
                newName: "IX_StudentCourseGrade_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourseGrades_CourseId",
                table: "StudentCourseGrade",
                newName: "IX_StudentCourseGrade_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourseGrade",
                table: "StudentCourseGrade",
                column: "StudentCourseGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseGrade_Courses_CourseId",
                table: "StudentCourseGrade",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseGrade_Students_StudentId",
                table: "StudentCourseGrade",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
