using System;
using Colegio.Datos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Colegio.Datos
{
    public partial class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Classrooms> Classrooms { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<StudentState> StudentState { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classrooms>(entity =>
            {
                entity.ToTable("CLASSROOMS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GradeId).HasColumnName("grade_id");

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.Property(e => e.Vacancies).HasColumnName("vacancies");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Classrooms)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLASSROOMS_GRADE");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Classrooms)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLASSROOMS_SECTION");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DOCUMENT_TYPE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DocumentType1)
                    .IsRequired()
                    .HasColumnName("document_type")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("GRADE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Grade1)
                    .IsRequired()
                    .HasColumnName("grade")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("SECTION");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Section1)
                    .IsRequired()
                    .HasColumnName("section")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudentState>(entity =>
            {
                entity.ToTable("STUDENT_STATE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StudentState1)
                    .IsRequired()
                    .HasColumnName("student_state")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.ToTable("STUDENTS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DocumentNumber)
                    .IsRequired()
                    .HasColumnName("document_number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentStateId).HasColumnName("student_state_id");

                entity.HasOne(d => d.StudentState)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StudentStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_State");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("USER_TYPE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserType1)
                    .IsRequired()
                    .HasColumnName("user_type")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DocumentNumber)
                    .IsRequired()
                    .HasColumnName("document_number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserType).HasColumnName("user_type");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_DocumentType");

                entity.HasOne(d => d.UserTypeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
