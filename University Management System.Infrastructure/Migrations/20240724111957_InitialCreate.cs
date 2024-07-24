using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace ClassLibrary1University_Management_System.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE SEQUENCE IF NOT EXISTS \"Users_id_seq\" START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;");
            migrationBuilder.Sql("CREATE SEQUENCE IF NOT EXISTS \"Class_Id_seq\" START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;");
            migrationBuilder.Sql("CREATE SEQUENCE IF NOT EXISTS \"Class_TeacherId_seq\" START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;");
            migrationBuilder.Sql("CREATE SEQUENCE IF NOT EXISTS \"Class_CourseId_seq\" START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;");
            migrationBuilder.Sql("CREATE SEQUENCE IF NOT EXISTS \"ClassSessions_Id_seq\" START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;");
            migrationBuilder.Sql("CREATE SEQUENCE IF NOT EXISTS \"ClassSessions_ClassId_seq\" START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;");
            migrationBuilder.Sql("CREATE SEQUENCE IF NOT EXISTS \"ClassSessions_SessionTimeId_seq\" START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MaxStudentsNumber = table.Column<int>(type: "integer", nullable: true),
                    EnrolmentDateRange = table.Column<NpgsqlRange<DateOnly>>(type: "daterange", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("courses_pk", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionTime",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sessiontime_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"Users_id_seq\"'::regclass)"),
                    Name = table.Column<string>(type: "character varying", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pk", x => x.Id);
                    table.ForeignKey(
                        name: "users_role_id_fk",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"Class_Id_seq\"'::regclass)"),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"Class_TeacherId_seq\"'::regclass)"),
                    CourseId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"Class_CourseId_seq\"'::regclass)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("class_pk", x => x.Id);
                    table.ForeignKey(
                        name: "class_course_id_fk",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "class_teacher_id_fk",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassEnrollment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("classenrollment_pk", x => x.Id);
                    table.ForeignKey(
                        name: "classenrollment_class_id_fk",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "classenrollment_users_id_fk",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"ClassSessions_Id_seq\"'::regclass)"),
                    TeacherPerCourseId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"ClassSessions_ClassId_seq\"'::regclass)"),
                    SessionTimeId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"ClassSessions_SessionTimeId_seq\"'::regclass)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("classsessions_pk", x => x.Id);
                    table.ForeignKey(
                        name: "classsessions_class_id_fk",
                        column: x => x.TeacherPerCourseId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "classsessions_sessiontime_id_fk",
                        column: x => x.SessionTimeId,
                        principalTable: "SessionTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "class_id_uindex",
                table: "Class",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Class_CourseId",
                table: "Class",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_TeacherId",
                table: "Class",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "classenrollment_id_uindex",
                table: "ClassEnrollment",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollment_ClassId",
                table: "ClassEnrollment",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollment_StudentId",
                table: "ClassEnrollment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "courses_\"name\"_uindex",
                table: "Courses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "roles_\"id\"_uindex",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "roles_\"name\"_uindex",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "classsessions_id_uindex",
                table: "Session",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_SessionTimeId",
                table: "Session",
                column: "SessionTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_TeacherPerCourseId",
                table: "Session",
                column: "TeacherPerCourseId");

            migrationBuilder.CreateIndex(
                name: "sessiontime_id_uindex",
                table: "SessionTime",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "users_\"email\"_uindex",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_\"id\"_uindex",
                table: "Users",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassEnrollment");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "SessionTime");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            // Drop the sequence when rolling back the migration
            migrationBuilder.Sql("DROP SEQUENCE IF EXISTS \"Users_id_seq\";");
        }
    }
}
