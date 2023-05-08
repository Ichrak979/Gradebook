using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Models.Gradebook;

public partial class GradebookDbContext : DbContext
{
    public GradebookDbContext()
    {
    }

    public GradebookDbContext(DbContextOptions<GradebookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:GradebookCS");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grade__3214EC076C5D7881");

            entity.ToTable("Grade");

            entity.Property(e => e.Grade1).HasColumnName("Grade");
            entity.Property(e => e.Subject).HasMaxLength(30);

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grade_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC0776236287");

            entity.ToTable("Student");

            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Firstname).HasMaxLength(30);
            entity.Property(e => e.Lastname).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
