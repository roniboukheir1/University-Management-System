﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using University_Management_System.Infrastructure;

#nullable disable

namespace ClassLibrary1University_Management_System.Infrastructure.Migrations
{
    [DbContext(typeof(UmsContext))]
    [Migration("20240724125053_AddStudentEntity2")]
    partial class AddStudentEntity2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("University_Management_System.Domain.Models.Class", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"Class_Id_seq\"'::regclass)");

                    b.Property<long>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"Class_CourseId_seq\"'::regclass)");

                    b.Property<long>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"Class_TeacherId_seq\"'::regclass)");

                    b.HasKey("Id")
                        .HasName("class_pk");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.HasIndex(new[] { "Id" }, "class_id_uindex")
                        .IsUnique();

                    b.ToTable("Class", (string)null);
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.ClassEnrollment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("classenrollment_pk");

                    b.HasIndex("ClassId");

                    b.HasIndex("StudentId");

                    b.HasIndex(new[] { "Id" }, "classenrollment_id_uindex")
                        .IsUnique();

                    b.ToTable("ClassEnrollment", (string)null);
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Course", b =>
                {
                    b.Property<long>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("CourseId"));

                    b.Property<NpgsqlRange<DateOnly>?>("EnrolmentDateRange")
                        .HasColumnType("daterange");

                    b.Property<int?>("MaxStudentsNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("CourseId")
                        .HasName("courses_pk");

                    b.HasIndex(new[] { "Name" }, "courses_\"name\"_uindex")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("roles_pk");

                    b.HasIndex(new[] { "Id" }, "roles_\"id\"_uindex")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "roles_\"name\"_uindex")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Session", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"ClassSessions_Id_seq\"'::regclass)");

                    b.Property<long>("SessionTimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"ClassSessions_SessionTimeId_seq\"'::regclass)");

                    b.Property<long>("TeacherPerCourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"ClassSessions_ClassId_seq\"'::regclass)");

                    b.HasKey("Id")
                        .HasName("classsessions_pk");

                    b.HasIndex("SessionTimeId");

                    b.HasIndex("TeacherPerCourseId");

                    b.HasIndex(new[] { "Id" }, "classsessions_id_uindex")
                        .IsUnique();

                    b.ToTable("Session", (string)null);
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.SessionTime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("sessiontime_pk");

                    b.HasIndex(new[] { "Id" }, "sessiontime_id_uindex")
                        .IsUnique();

                    b.ToTable("SessionTime", (string)null);
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"Users_id_seq\"'::regclass)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying");

                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("users_pk");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Email" }, "users_\"email\"_uindex")
                        .IsUnique();

                    b.HasIndex(new[] { "Id" }, "users_\"id\"_uindex")
                        .IsUnique();

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Student", b =>
                {
                    b.HasBaseType("University_Management_System.Domain.Models.User");

                    b.Property<double>("AverageGrade")
                        .HasColumnType("double precision");

                    b.Property<bool>("CanApplyToFrance")
                        .HasColumnType("boolean");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Class", b =>
                {
                    b.HasOne("University_Management_System.Domain.Models.Course", "Course")
                        .WithMany("TeacherPerCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("class_course_id_fk");

                    b.HasOne("University_Management_System.Domain.Models.User", "Teacher")
                        .WithMany("TeacherPerCourses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("class_teacher_id_fk");

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.ClassEnrollment", b =>
                {
                    b.HasOne("University_Management_System.Domain.Models.Class", "Class")
                        .WithMany("ClassEnrollments")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("classenrollment_class_id_fk");

                    b.HasOne("University_Management_System.Domain.Models.User", "Student")
                        .WithMany("ClassEnrollments")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("classenrollment_users_id_fk");

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Session", b =>
                {
                    b.HasOne("University_Management_System.Domain.Models.SessionTime", "SessionTime")
                        .WithMany("TeacherPerCoursePerSessionTimes")
                        .HasForeignKey("SessionTimeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("classsessions_sessiontime_id_fk");

                    b.HasOne("University_Management_System.Domain.Models.Class", "Class")
                        .WithMany("TeacherPerCoursePerSessionTimes")
                        .HasForeignKey("TeacherPerCourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("classsessions_class_id_fk");

                    b.Navigation("Class");

                    b.Navigation("SessionTime");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.User", b =>
                {
                    b.HasOne("University_Management_System.Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("users_role_id_fk");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Student", b =>
                {
                    b.HasOne("University_Management_System.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Class", b =>
                {
                    b.Navigation("ClassEnrollments");

                    b.Navigation("TeacherPerCoursePerSessionTimes");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Course", b =>
                {
                    b.Navigation("TeacherPerCourses");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.SessionTime", b =>
                {
                    b.Navigation("TeacherPerCoursePerSessionTimes");
                });

            modelBuilder.Entity("University_Management_System.Domain.Models.User", b =>
                {
                    b.Navigation("ClassEnrollments");

                    b.Navigation("TeacherPerCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
