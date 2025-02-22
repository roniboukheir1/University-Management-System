﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using University_Management_System.Domain.Models;

namespace University_Management_System.Infrastructure
{
    public partial class UmsContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly List<TenantConfiguration> _tenantConfigs;
        public UmsContext()
        {
        }

        public UmsContext(DbContextOptions<UmsContext> options, IHttpContextAccessor httpContextAccessor,
            List<TenantConfiguration> tenantConfigs)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantConfigs = tenantConfigs;
        }

        public virtual DbSet<ClassEnrollment> ClassEnrollments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SessionTime> SessionTimes { get; set; }
        public virtual DbSet<Class> TeacherPerCourses { get; set; }
        public virtual DbSet<Session> TeacherPerCoursePerSessionTimes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentCourseGrade> StudentCourseGrades { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseNpgsql("Host=localhost;Database=ums;Username=postgres;Password=mysequel1!");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var tenantId = _httpContextAccessor.HttpContext?.Items["TenantId"]?.ToString();
            if (!string.IsNullOrEmpty(tenantId))
            {
                var tenantConfig = _tenantConfigs.FirstOrDefault(t => t.TenantId == tenantId);
                if (tenantConfig != null)
                {
                    foreach (var entity in modelBuilder.Model.GetEntityTypes())
                    {
                        entity.SetSchema(tenantConfig.Schema);
                    }
                }
            }
            modelBuilder.Entity<StudentCourseGrade>()
                .HasKey(scg => scg.StudentCourseGradeId);

            modelBuilder.Entity<StudentCourseGrade>()
                .HasOne(scg => scg.Student)
                .WithMany(s => s.StudentCourseGrades)
                .HasForeignKey(scg => scg.StudentId);

            modelBuilder.Entity<StudentCourseGrade>()
                .HasOne(scg => scg.Course)
                .WithMany()
                .HasForeignKey(scg => scg.CourseId);
            modelBuilder.Entity<ClassEnrollment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("classenrollment_pk");

                entity.ToTable("ClassEnrollment");

                entity.HasIndex(e => e.Id, "classenrollment_id_uindex").IsUnique();

                entity.Property(e => e.ClassId).ValueGeneratedOnAdd();
                entity.Property(e => e.StudentId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Class).WithMany(p => p.ClassEnrollments)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("classenrollment_class_id_fk");

                entity.HasOne(d => d.Student).WithMany(p => p.ClassEnrollments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("classenrollment_users_id_fk");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId).HasName("courses_pk");

                entity.HasIndex(e => e.Name, "courses_\"name\"_uindex").IsUnique();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("roles_pk");

                entity.HasIndex(e => e.Id, "roles_\"id\"_uindex").IsUnique();

                entity.HasIndex(e => e.Name, "roles_\"name\"_uindex").IsUnique();
            });

            modelBuilder.Entity<SessionTime>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("sessiontime_pk");

                entity.ToTable("SessionTime");

                entity.HasIndex(e => e.Id, "sessiontime_id_uindex").IsUnique();

                entity.Property(e => e.EndTime).HasColumnType("timestamp without time zone");
                entity.Property(e => e.StartTime).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("class_pk");

                entity.ToTable("Class");

                entity.HasIndex(e => e.Id, "class_id_uindex").IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Class_Id_seq\"'::regclass)");
                entity.Property(e => e.CourseId).HasDefaultValueSql("nextval('\"Class_CourseId_seq\"'::regclass)");
                entity.Property(e => e.TeacherId).HasDefaultValueSql("nextval('\"Class_TeacherId_seq\"'::regclass)");

                entity.HasOne(d => d.Course).WithMany(p => p.TeacherPerCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("class_course_id_fk");

                entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherPerCourses)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("class_teacher_id_fk");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("classsessions_pk");

                entity.ToTable("Session");

                entity.HasIndex(e => e.Id, "classsessions_id_uindex").IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"ClassSessions_Id_seq\"'::regclass)");
                entity.Property(e => e.SessionTimeId).HasDefaultValueSql("nextval('\"ClassSessions_SessionTimeId_seq\"'::regclass)");
                entity.Property(e => e.TeacherPerCourseId).HasDefaultValueSql("nextval('\"ClassSessions_ClassId_seq\"'::regclass)");

                entity.HasOne(d => d.SessionTime).WithMany(p => p.TeacherPerCoursePerSessionTimes)
                    .HasForeignKey(d => d.SessionTimeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("classsessions_sessiontime_id_fk");

                entity.HasOne(d => d.Class).WithMany(p => p.TeacherPerCoursePerSessionTimes)
                    .HasForeignKey(d => d.TeacherPerCourseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("classsessions_class_id_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pk");

                entity.HasIndex(e => e.Email, "users_\"email\"_uindex").IsUnique();

                entity.HasIndex(e => e.Id, "users_\"id\"_uindex").IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Users_id_seq\"'::regclass)");
                entity.Property(e => e.Name).HasColumnType("character varying");
                entity.Property(e => e.RoleId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("users_role_id_fk");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");

                entity.Property(e => e.AverageGrade).HasColumnType("double precision");
                entity.Property(e => e.CanApplyToFrance).HasColumnType("boolean");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}