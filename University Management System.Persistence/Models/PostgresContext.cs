using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University_Management_System.Persistence.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }
    
    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }
    
    public virtual DbSet<TimeSlotModel> TimeSlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=mysequel1!;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");
        
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.startOfRegistration).IsRequired();
            entity.Property(e => e.endOfRegistration).IsRequired();
            entity.Property(e => e.maxStudents).IsRequired();
            entity.HasOne(e => e.Teacher)
                .WithMany(t => t.Classes)
                .HasForeignKey(e => e.TeacherId);
            entity.HasMany(e => e.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.ClassId);
        });
        
        modelBuilder.Entity<TimeSlotModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DayPattern).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.ClassId });

            entity.HasIndex(e => e.ClassId, "IX_Enrollments_ClassId");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments).HasForeignKey(d => d.ClassId);

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments).HasForeignKey(d => d.StudentId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
