﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using University_Management_System.Persistence.Models;

#nullable disable

namespace University_Management_System.Persistence.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20240722073616_AddCourseTable1")]
    partial class AddCourseTable1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pg_catalog", "adminpack");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("University_Management_System.Persistence.Models.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<int>("Credits")
                        .HasColumnType("integer");

                    b.Property<long>("TeacherId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("endOfRegistration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("maxStudents")
                        .HasColumnType("integer");

                    b.Property<DateTime>("startOfRegistration")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.Enrollment", b =>
                {
                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<long>("ClassId")
                        .HasColumnType("bigint");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.HasKey("StudentId", "ClassId");

                    b.HasIndex(new[] { "ClassId" }, "IX_Enrollments_ClassId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.Teacher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.TimeSlotModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClassId")
                        .HasColumnType("bigint");

                    b.Property<string>("DayPattern")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("TimeSlots");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.Course", b =>
                {
                    b.HasOne("University_Management_System.Persistence.Models.Teacher", "Teacher")
                        .WithMany("Classes")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.Enrollment", b =>
                {
                    b.HasOne("University_Management_System.Persistence.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University_Management_System.Persistence.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.TimeSlotModel", b =>
                {
                    b.HasOne("University_Management_System.Persistence.Models.Course", "Course")
                        .WithMany("TimeSlots")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.Course", b =>
                {
                    b.Navigation("Enrollments");

                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.Student", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("University_Management_System.Persistence.Models.Teacher", b =>
                {
                    b.Navigation("Classes");
                });
#pragma warning restore 612, 618
        }
    }
}